﻿using BookApp.Helper;
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
    [System.Web.Http.RoutePrefix("api/books")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class BookController : ApiController
    {
        private const int ErrorCodeNoBookFound = 1;
        private const int ErrorCodeSavingBook = 2;
        private const int ErrorCodeUpdatingBook = 3;
        private const int ErrorCodeDeletingBook = 4;
        private IBookService BookService;

        public BookController() {
        }
        public BookController(IBookService bookService) {
            BookService = bookService;
        }

        [HttpGet]
        [Route("GetBookById")]
        public HttpResponseMessage GetBookById(Guid bookId) {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            // Coding convention: always use braces even on oneliner ifs and elses
            if (book != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeNoBookFound, "No book found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("CreateBook")]
        public HttpResponseMessage SaveBook([FromBody]Book book) {
            if (book == null)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.AddBook(book);
            if (book != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeSavingBook, "Error Saving Book", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public HttpResponseMessage UpdateBook([FromBody]Book book) {
            if (book == null)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.UpdateBook(book);
            if (book != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            }
            else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeUpdatingBook, "Error Updating Book", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("DeleteBook")]
        public HttpResponseMessage DeleteBook([FromBody]Guid bookId) {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            if (book != null) {
                var result = BookService.DeleteBook(book);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted", JsonFormatter);
                }
                else
                {
                    // No magical numbers in code - error codes must be defined as constants
                    // Immediately turns out that bad error code is used
                    throw new APIDataException(ErrorCodeDeletingBook, "Error Deleting Book", HttpStatusCode.NotFound);
                }
            } else
            {
                // No magical numbers in code - error codes must be defined as constants
                throw new APIDataException(ErrorCodeNoBookFound, "No book found", HttpStatusCode.NotFound);
            }
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
