using FastMember;
using MySqlConnector;
using System.Collections;
using System.Net;

namespace CricketAPI
{
    public static class Common
    {
        public static T ConvertToObject<T>(this MySqlDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < rd.FieldCount; i++)
            {
                if (!rd.IsDBNull(i))
                {
                    string fieldName = rd.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[t, fieldName] = rd.GetValue(i);
                    }
                }
            }

            return t;
        }

        public static void CreateResponse<T>(HttpStatusCode code, string status, string message, out Response actionResponse, T objResponse) where T : IList
        {
            actionResponse = new Response()
            {
                Code = code,
                Status = status,
                Message = message,
                Data = objResponse
            };

        }
        public static void CreateResponse(HttpStatusCode code, string status, string message, out Response actionResponse)
        {
            actionResponse = new Response()
            {
                Code = code,
                Status = status,
                Message = message,
                Data = null
            };

        }
        public static void CreateErrorResponse<T>(HttpStatusCode code, out Response actionResponse, T objResponse) where T : Exception
        {
            var ex = (Exception)objResponse;
            actionResponse = new Response()
            {
                Code = code,
                Status = "Error occured while processing your request",
                Message = ex.Message,
                Data = null
            };

        }
    }
}
