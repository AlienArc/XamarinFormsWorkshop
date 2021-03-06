﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZombiepediaService.Services;

namespace ZombiepediaService.Controllers
{
    public class CommentController : ApiController
    {
        [Route("api/comment/{zombieId}")]
        public IEnumerable<string> Get(int zombieId)
        {
            return ZombieDataService.GetComments(zombieId);
        }

	    [HttpGet]
	    [Route("api/addcomment")]
	    public IHttpActionResult AddComment(int zombieId, string comment)
	    {
		    //var valueArray = value.Split('|');
			//ZombieDataService.AddComment(Int32.Parse(valueArray[0]), valueArray[1]);
			ZombieDataService.AddComment(zombieId, comment);
			return Ok();
	    }
    }
}
