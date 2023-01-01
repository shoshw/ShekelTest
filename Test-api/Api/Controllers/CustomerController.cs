using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;
using Bl;


namespace Api.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("AddCustomer")]
        public Response AddCustomer([FromBody]  CustomerToAddDTO customerToAddDTO)
        {
            Response result = new Response();
            try
            {
                new CustomerBl().AddNewCustomer(customerToAddDTO);
                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message} try again";
                result.StatusCode = HttpStatusCode.Unauthorized;
            }
            return result;
        }

        [HttpGet]
        [Route("GetGroup")]
        public Response GetGroupsDetailes()
        {
            Response result = new Response();
            try
            {
                result.IsSuccess = true;
                result.StatusCode = HttpStatusCode.OK;
                result.Data= new CustomerBl().GetGroupsDetailes();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message} try again";
                result.StatusCode = HttpStatusCode.Unauthorized;
            }
            return result;
        }
    }
}
