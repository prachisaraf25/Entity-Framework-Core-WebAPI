using DBOpeartionsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DBOpeartionsWithEFCoreApp.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly AppDbContext _appDbContext;
        public BooksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Insert/ Add new records in the Entity Framework Core
        [HttpPost("")]
        public async Task<IActionResult> InsertBookRecords([FromBody] Book model)
        {
            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        //Get all records from the Book table
        [HttpGet("")]
        public async Task<IActionResult> GetAllBokksRecords()
        {
            var result = await (from Book in _appDbContext.Books
                                select Book).ToListAsync();
            return Ok(result);
        }

        //Get related data using Navigational Properties
        [HttpGet("getRelatedData")]
        public async Task<IActionResult> GetRelatedData()
        {
            var result = await _appDbContext.Books.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                //Language = x.Language != null ? x.Language.Title : "NA"
            }).ToListAsync();
            return Ok(result);
        }

        ////Eager Loading
        //[HttpGet("getData")]
        //public async Task<IActionResult> GetRelatedDataUsingEagerLoading()
        //{
        //    var result = await _appDbContext.Books.Include(x => x.Language).ToListAsync();
        //    return Ok(result);
        //}

        ////Eager Loading ----> One to many
        //[HttpGet("getLanguageData")]
        //public async Task<IActionResult> GetRelateLanguageDataUsingEagerLoading()
        //{
        //    var result = await _appDbContext.Languages.Include(x => x.Books).ToListAsync();
        //    return Ok(result);
        //}

        ////Explicit Loading in EF Core
        //[HttpGet("getDataUsingExplicitLoading")]
        //public async Task<IActionResult> GetDataUsingExplicitLoading()
        //{
        //    var book = await _appDbContext.Books.FirstAsync();
        //    await _appDbContext.Entry(book).Reference(x => x.Language).LoadAsync();
        //    return Ok(book);
        //}

        ////Explicit Loading in EF Core ----> One to many
        //[HttpGet("getDataUsingExplicitLoading")]
        //public async Task<IActionResult> GetLanguageDataUsingExplicitLoading()
        //{
        //    var languages = await _appDbContext.Languages.ToListAsync();
        //    foreach(var language in languages)
        //    {
        //        await _appDbContext.Entry(language).Collection(x=>x.Books).LoadAsync();
        //    }
        //    return Ok(languages);
        //}

        ////Get data using SQL Queries
        //[HttpGet("getSQLQueryData")]
        //public async Task<IActionResult> GetSQLQueryData()
        //{
        //    var columnName = "Title";
        //    var Title = "Test Book 7Updated Updated";
        //    var books = await _appDbContext.Books.FromSqlRaw($"Select * from Books where {columnName} = '{Title}'").ToListAsync();
        //    return Ok(books);
        //}

        //Get data using SQL Stored Procedure
        [HttpGet("getSQLQueryData")]
        public async Task<IActionResult> GetSQLQueryData()
        {
            //Without Parameter Query
            //var books = await _appDbContext.Books.FromSql($"EXEC SP_GetAllBooks").ToListAsync();

            //With Parameter Query
            var parameter = new SqlParameter("@BookID", 8);
            var books = await _appDbContext.Books.FromSql($"EXEC SP_GetBookByID {parameter}").ToListAsync();
            return Ok(books);
        }

        ////Execute SQL Queries on database using Entity Framework Core
        //[HttpGet("getSQLQueryData")]
        //public async Task<IActionResult> GetSQLQueryData()
        //{
        //    //var books = await _appDbContext.Database.SqlQuery<Book>($"SELECT * FROM Books").ToListAsync();
        //    var books = await _appDbContext.Database.ExecuteSqlAsync($"UPDATE Books SET NoOfPages = 1000 WHERE ID = 8");
        //    return Ok(books);
        //}

        //Bulk Insert in the Books Table
        [HttpPost("bulkInsert")]
        public async Task<IActionResult> BulkInsertIntoTable([FromBody] List<Book> model)
        {
            await _appDbContext.Books.AddRangeAsync(model);
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        //Update specific data in the table
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData([FromRoute] int id, [FromBody] Book model)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.NoOfPages = model.NoOfPages;

            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        //Update specific data in the table using a single command
        [HttpPut("")]
        public async Task<IActionResult> UpdateDataUsingSingleCommand([FromBody] Book model)
        {
            _appDbContext.Books.Update(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        //Bulk Update in the database
        [HttpPut("bulkUpdate")]
        public async Task<IActionResult> UpdateDataBulk()
        {
            var book = await _appDbContext.Books
                .Where(x => x.NoOfPages > 100)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.Title, p => p.Title + " Updated"));
            return Ok();
        }

        //Delete a specific data from a table
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData([FromRoute] int id)
        {
            var book = new Book { Id = id };
            _appDbContext.Entry(book).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();

            //var book = await _appDbContext.Books.FindAsync(id);

            //if (book == null)
            //{
            //    return NotFound();
            //}

            //_appDbContext.Books.Remove(book);
            //await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        //Delete data in bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteDataInBulk()
        {
            //Requires more database calls
            //var books = await _appDbContext.Books.Where(x => x.Id < 6).ToListAsync();

            //_appDbContext.Books.RemoveRange(books);
            //await _appDbContext.SaveChangesAsync();

            var books = await _appDbContext.Books.Where(x => x.Id < 8).ExecuteDeleteAsync();
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

