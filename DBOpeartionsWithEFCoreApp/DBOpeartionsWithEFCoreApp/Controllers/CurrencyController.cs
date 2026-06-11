using DBOpeartionsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOpeartionsWithEFCoreApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var result = await _appDbContext.Currencies.ToListAsync();
            //var result = from currencies in _appDbContext.Currencies
            //             select currencies;
            //var result = await _appDbContext.Currencies.ToListAsync();

            //Getting all columns
            //var result = await (from currencies in _appDbContext.Currencies
            //                    select currencies).ToListAsync();

            //Getting only the specific columns
            //var result = await (from currencies in _appDbContext.Currencies
            //                    select new Currency()
            //                    {
            //                        Id = currencies.Id,
            //                        Title = currencies.Title
            //                    }).ToListAsync();

            //Getting only the specific columns using anonymous functions
            var result = await (from currencies in _appDbContext.Currencies
                                select new
                                {
                                    CurrencyId = currencies.Id,
                                    Name = currencies.Title
                                }).AsNoTracking().ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        //Getting one record only using one parameter
        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name)
        //{
        //    //FirstAsync() and FirstOrDefaultAsync()    ----> Does not give error if more than one records exists of the same name, returns the first value found
        //    //var result = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync();
        //    //var result = await _appDbContext.Currencies.Where(x => x.Title == name).FirstOrDefaultAsync();

        //    //More Performance Friendly
        //    //var result = await _appDbContext.Currencies.FirstAsync(x => x.Title == name);
        //    //var result = await _appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == name);

        //    //SingleAsync() and SingleOrDefaultAsync()    ----> Gives error if more than one records exists of the same name
        //    //var result = await _appDbContext.Currencies.Where(x => x.Title == name).SingleAsync();
        //    //var result = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync();


        //    //More Performance Friendly
        //    //var result = await _appDbContext.Currencies.SingleAsync(x => x.Title == name);
        //    var result = await _appDbContext.Currencies.SingleOrDefaultAsync(x => x.Title == name);
        //    return Ok(result);
        //}

        //Getting one record using multiple parameters
        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetCurrencyByNameAndDescAsync([FromRoute] string name, [FromQuery] string? description)
        //{
        //    var result = await _appDbContext.Currencies.FirstOrDefaultAsync(
        //        x => x.Title == name 
        //        && (string.IsNullOrEmpty(description) || x.Description == description));
        //    return Ok(result);
        //}

        //Getting all records using multiple parameters
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllCurrencyByNameAndDescAsync([FromRoute] string name, [FromQuery] string? description)
        {
            var result = await _appDbContext.Currencies.Where(
                x => x.Title == name
                && (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync();
            return Ok(result);
        }

        //Getting all records based on the ids [1,2,3,....n]
        [HttpPost("all")]
        public async Task<IActionResult> GetAllRecordsBasedOnIds([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1, 2, 3 };
            var result = await _appDbContext.Currencies.Where(
                x => ids.Contains(x.Id)
                ).ToListAsync();
            return Ok(result);
        }

        //Select specific columns for all records based on the ids [1,2,3,....n]
        [HttpPost("specific")]
        public async Task<IActionResult> GetSpecificColumnsBasedOnIds([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1, 2, 3 };
            var result = await _appDbContext.Currencies.Where(
                x => ids.Contains(x.Id)
                ).Select(x => new Currency()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToListAsync();
            return Ok(result);
        }
    }
}
