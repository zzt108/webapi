﻿using BookApp.Helper;
using Interfaces.Repositories;
using Interfaces.Services;
using Models.DomainModels;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class UserController : ApiController
    {
        // todo Collides with book delete error code, book delete error code was not defined, decision needs to be done
        private const int ErrorCodeNoUserFound = 4;  
        private const int ErrorCodeSavingUser = 5;
        private const int ErrorCodeUpdatingUser = 6;
        private const int ErrorCodeDeletingUser = 7;
        // todo book error codes should come from book controllers
        private const int ErrorCodeSavingBook = 2;
        private const int ErrorCodeNoBooksFound = 1;
        private IBookRepository BookRepository;
        private IBookService BookService;
        private IUserService UserService;

        public UserController() {
        }
        public UserController(IBookRepository bookRepository, IUserService userService, IBookService bookService) {
            BookRepository = bookRepository;
            UserService = userService;
            BookService = bookService;
        }

        [HttpGet]
        [Route("GetUserById")]
        public HttpResponseMessage GetUserById(Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var user = UserService.GetUserById(userId);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeNoUserFound, "No user found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage SaveUser([FromBody]User user) {
            if (user == null)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid user object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            user = UserService.AddUser(user);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeSavingUser, "Error Saving User", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public HttpResponseMessage UpdateUser([FromBody]User user) {
            if (user == null)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid user object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            user = UserService.UpdateUser(user);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user, JsonFormatter);
            else
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeUpdatingUser, "Error Updating User", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser([FromBody]Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var user = UserService.GetUserById(userId);
            if (user != null) {
                var result = UserService.DeleteUser(user);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted", JsonFormatter);
                }
                else
                {
                    // No magical numbers in code - error codes must be defined as constants
                    throw new APIDataException(ErrorCodeDeletingUser, "Error Deleting User", HttpStatusCode.NotFound);
                }
            } else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeNoUserFound, "No user found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("CreateUserBook")]
        public HttpResponseMessage SaveBook([FromUri]Guid userId, [FromBody]Book book) {
            if (book == null)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            if (userId == null || userId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            BookRepository.Add(book);
            BookRepository.SaveChanges();
            var result = BookRepository.GetBookByID(book.Id);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                //todo SavingBook error code should come from book controller
                throw new APIDataException(ErrorCodeSavingBook, "Error Saving Book", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetUserBooks")]
        public HttpResponseMessage GetUserBooks(Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var books = BookService.GetBooksByUserId(userId);
            if (books != null)
                return Request.CreateResponse(HttpStatusCode.OK, books, JsonFormatter);
            else
                // No magical numbers in code - error codes must be defined as constants
                //todo NoBooksFound error code should come from book controller
                throw new APIDataException(ErrorCodeNoBooksFound, "No books found", HttpStatusCode.NotFound);
        }



        protected JsonMediaTypeFormatter JsonFormatter {
            get {
                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;

                json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ContractResolver = new CamelCasePropertyNamesContractResolver();
                json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                return formatter;
            }

        }
    }
}
