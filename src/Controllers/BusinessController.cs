using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]

[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IRepository<Business> _businessRepository;
    public BusinessController(IRepository<Business> businessRepository)
    {
        _businessRepository = businessRepository;
    }



    [HttpGet]
    public async Task<IActionResult> GetAll(string search = null)
    {
        if (search != null)
        {
            try
            {
                var searchResult = await _businessRepository.Search(search);
                return Ok(searchResult);
            }
            catch (Exception)
            {
                return BadRequest("The text you've entered is invalid.");
            }
        }
        try
        {
            var allBusinesses = await _businessRepository.GetAll();
            return Ok(allBusinesses);
        }
        catch (Exception)
        {
            return NotFound("There are no businesses.");
        }
    }
   

    
    [HttpGet]
    [Route("/{Auth}")]
    public async Task<IActionResult> GetbyBusiness(string Auth)
    {
        try{
        var returnedBusiness = await _businessRepository.GetbyBusiness(Auth);

            long id = returnedBusiness.Id;
            returnedBusiness.Products = await _businessRepository.GetProducts(id);


            
            return Ok(returnedBusiness);
        }
        catch (Exception error){
            Console.WriteLine(error);
            return BadRequest("cannot fulfil");
        }
    }



    [HttpGet]
    [Route("[action]/{Id}")]
    public async Task<IActionResult> GetbyProduct(long Id)
    {
        try{
        var returnedBusiness = await _businessRepository.GetbyProduct(Id);
        return Ok(returnedBusiness);
        }
        catch (Exception){
        return BadRequest("Id not found");
        }
    }


    
    [HttpPut]
    [Route("[action]/{Id}")]
    public async Task<IActionResult> UpdatebyBusiness(long id, [FromBody] Business business)
    {
        try{
            var updatedBusiness = await _businessRepository.UpdatebyBusiness(new Business {Id = id, BusinessName = business.BusinessName, PrimaryEmail = business.PrimaryEmail, AddrLocation = business.AddrLocation, TelephoneNumber = business.TelephoneNumber, BusinessLogo = business.BusinessLogo, IsTrading = business.IsTrading});
            return Ok(updatedBusiness);
        }
        catch(Exception){
            return BadRequest("Id not found");
        }
    }



    // [HttpPut]
    // [Route("[action]/{ProductId}")]
    // public async Task<IActionResult> UpdatebyProduct(int ProductId, [FromBody] Business business)
    // {
    //     try{
    //         // var updatedBusiness = await _businessRepository.UpdatebyProduct(new Business { ProductId =ProductId, ProductName = business.ProductName, ProductDescription = business.ProductDescription, ProductImage = business.ProductImage, ProductPrice = business.ProductPrice, Quantity= business.Quantity});
    //         //return Ok(updatedBusiness);
    //     }
    //     catch(Exception){
    //         return BadRequest("Id not found");
    //     }
    // }
    


    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> InsertbyBusiness([FromBody] Business business)
    {
        try 
        {
            var insertedBusiness = await _businessRepository.InsertbyBusiness(business);
            return Created($"/businesses/{insertedBusiness.Id}", insertedBusiness);
        }
        catch(Exception)
        {
            return BadRequest("Business entered is not valid");
        }
    }



    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> InsertbyProduct([FromBody] Business business)
    {
        try 
        {
            var insertedBusiness = await _businessRepository.InsertbyProduct(business);
            return Created($"/businesses/{insertedBusiness.Id}", insertedBusiness);
        }
        catch(Exception)
        {
            return BadRequest("Business entered is not valid");
        }
    }



    [HttpDelete]
    [Route("[action]/{Id}")]
    public IActionResult DeletebyBusiness(long Id)
    {
        try
        {
            _businessRepository.DeletebyBusiness(Id);
            return Ok($"Business at {Id} is deleted");
        }
        catch(Exception)
        {
            return BadRequest("Id is not valid");
        }
    }



    [HttpDelete]
    [Route("[action]/{ProductId}")]
    public IActionResult DeletebyProduct(long ProductId)
    {
        try
        {
            _businessRepository.DeletebyProduct(ProductId);
            return Ok($"Product at {ProductId} is deleted");
        }
        catch(Exception)
        {
            return BadRequest("Id is not valid");
        }
    }
}