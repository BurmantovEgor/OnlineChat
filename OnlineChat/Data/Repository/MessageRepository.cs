using CSharpFunctionalExtensions;
using Npgsql;
using OnlineChat.Core.DTO_s;
using OnlineChat.Data.Abstractions;
using OnlineChat.Data.Models;

namespace OnlineChat.Data.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;
        public MessageRepository(string connectionString) => _connectionString = connectionString;

        public async Task<Result> InsertMessage(MessageEntity message)
        {
            var conn = new NpgsqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand("INSERT INTO messages (id, content,date, message_num) " +
                    "VALUES (@id, @content, @date, @messageNumber)", conn);
                cmd.Parameters.AddWithValue("@id", message.id);
                cmd.Parameters.AddWithValue("@content", message.content);
                cmd.Parameters.AddWithValue("@date", message.date);
                cmd.Parameters.AddWithValue("@messageNumber", message.message_num);
                var result = await cmd.ExecuteNonQueryAsync();
                if (result == 0) return Result.Failure("Не удалось сохранить сообщение");
            }
            catch (Exception e)
            {
               return Result.Failure($"Не удалось сохранить сообщение. Ошибка: {e.ToString()}");
            }
            finally
            {
                await conn.CloseAsync();
            }
            return Result.Success();
        }

        public async Task<Result<List<MessageEntity>>> SelectMessage(DateTime start, DateTime end)
        {
            var conn = new NpgsqlConnection(_connectionString);
            List<MessageEntity> resultList = [];
            try
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand("select id, content, date, message_num from messages where date between @start and @end order by date", conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageEntity tempObj = new MessageEntity();
                    tempObj.id = reader.GetGuid(reader.GetOrdinal("id"));
                    tempObj.content = reader.GetString(reader.GetOrdinal("content"));
                    tempObj.date = reader.GetDateTime(reader.GetOrdinal("date"));
                    tempObj.message_num = reader.GetInt32(reader.GetOrdinal("message_num"));
                    resultList.Add(tempObj);
                }
                if (resultList.Count == 0) return Result.Failure<List<MessageEntity>>("За указанный промежуток времени нет сообщений");
            }
            catch (Exception e)
            {
                return Result.Failure<List<MessageEntity>>($"Не удалось загрузить сообщения. Ошибка: {e.ToString()}");
            }
            finally
            {
                await conn.CloseAsync();
            }
            return Result.Success(resultList);
        }
    }
}
