using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    public class DiffController : ApiController
    {
        DiffRepository _diff = DiffRepository.GetInstance();

        /// <summary>
        /// This method is used to post base64Data on the left side to be compared, using void method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="base64Data"></param>
        [Route("v1/diff/{id}/left")]
        public void PostLeft(string id, [FromBody]string base64Data)
        {
            _diff.SaveLeft(id, base64Data);
        }

        /// <summary>
        /// This method is used to post base64Data on the left side to be compared, using HttpResponseMessage method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="base64Data"></param>
        /// <returns></returns>
        [Route("v1/diff/{id}/leftV2")]
        public string PostLeftV2(string id, [FromBody]string base64Data)
        {
            if (id == null)
            {
                return "Id can not be null";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id can not be null");
            }
            else if (base64Data == null)
            {
                return "Text can not be null";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Text can not be null");
            }
            else if (!IsBase64String(base64Data))
            {
                return "Text must be in base64 format";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Text must be in base64 format");
            }
            else
            {
                _diff.SaveLeft(id, base64Data);
                return base64Data + " was saved on left side";
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, base64Data + " was saved on left side");
                //return response;
            }
        }

        /// <summary>
        /// This method is used to post base64Data on the right side to be compared, using void method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="base64Data"></param>
        [Route("v1/diff/{id}/right")]
        public void PostRight(string id, [FromBody]string base64Data)
        {
            _diff.SaveRight(id, base64Data);
        }

        /// <summary>
        /// This method is used to post base64Data on the right side to be compared, using HttpResponseMessage method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="base64Data"></param>
        /// <returns></returns>
        [Route("v1/diff/{id}/rightV2")]
        public string PostRightV2(string id, [FromBody]string base64Data)
        {
            if (id == null)
            {
                return "Id can not be null";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id can not be null");
            }
            else if (base64Data == null)
            {
                return "Text can not be null";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Text can not be null");
            }
            else if (!IsBase64String(base64Data))
            {
                return "Text must be in base64 format";
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Text must be in base64 format");
            }
            else
            {
                _diff.SaveRight(id, base64Data);
                return base64Data + " was saved on right side";
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, base64Data + " was saved on left side");
                //return response;
            }
        }

        ///<summary>
        /// This method is used to compare if base64Data:
        ///     1) Contains the same value from left and right side
        ///     2) Contains the same size
        ///     3) Contains the same size and different values will return all differences
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("v1/diff/{id}")]
        public string Get(string id)
        {
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

        /// <summary>
        /// This method is used to validate if string is configured in base64 format
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsBase64String(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
