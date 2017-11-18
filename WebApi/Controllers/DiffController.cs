using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    public class DiffController : ApiController
    {
        DiffRepository _diff = DiffRepository.GetInstance();

        [Route("v1/diff/{id}/left")]
        public void PostLeft(string id, [FromBody]string base64Data)
        {
            _diff.SaveLeft(id, base64Data);
        }

        [Route("v1/diff/{id}/right")]
        public void PostRight(string id, [FromBody]string base64Data)
        {
            _diff.SaveRight(id, base64Data);
        }

        [Route("v1/diff/{id}")]
        public string Get(string id)
        {

            testing broken build;

            var diff = _diff.Get(id);
            if (diff != null)
            {
                if (diff.Left.Equals(diff.Right))
                {
                    return "same value";
                }
                else if (diff.Left == null || diff.Right == null || diff.Left.Length != diff.Right.Length)
                {
                    return "left and right aren't same size";
                }
                else
                {
                    List<string> differences = new List<string>();
                    for (int i = 0; i < diff.Left.Length; i++)
                    {
                        if (diff.Left[i] != diff.Right[i])
                        {
                            differences.Add("Difference found in position " + i + " left side: " + diff.Left[i] + " and right side: " + diff.Right[i]);
                        }
                    }
                    return string.Join("; ", differences);
                }
            }
            else
            {
                return "This Id doesn't exist";
            }

        }

        private string DecodeBase64(string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
    }
}
