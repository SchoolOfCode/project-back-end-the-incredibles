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
    //Get -> GetAll
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
    //Get{Id} -> Get(id)
    // [HttpGet("{Id}")]
    // public async Task<IActionResult> GetById(long Id)
    // {
    //     try{
    //     var returnedBusiness = await _businessRepository.Get(Id);
    //     return Ok(returnedBusiness);
    //     }
    //     catch (Exception){
    //     return BadRequest("Id not found");
    //     }
    // }
    
    [HttpGet]
    [Route("[action]/{Id}")]
    public async Task<IActionResult> GetbyBusiness(long Id)
    {
        try{
        var returnedBusiness = await _businessRepository.GetbyBusiness(Id);
        return Ok(returnedBusiness);
        }
        catch (Exception){
        return BadRequest("Id not found");
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

    //Put{id} -> Update(business)
    [HttpPut]
    [Route("[action]/{Id}")]
    public async Task<IActionResult> UpdatebyBusiness(long id, [FromBody] Business business)
    {
        try{
            var updatedBusiness = await _businessRepository.UpdatebyBusiness(new Business {Id = id, BusinessName = business.BusinessName, PrimaryContact = business.PrimaryContact, AddrBuildingNumber = business.AddrBuildingNumber, AddrBuildingName = business.AddrBuildingName, AddrStreet = business.AddrStreet, AddrCity = business.AddrCity, AddrCounty = business.AddrCounty, AddrPostcode = business.AddrPostcode, TelephoneNumber = business.TelephoneNumber, TwitterHandle = business.TwitterHandle, SocialmediaLink = business.SocialmediaLink, BusinessImage = business.BusinessImage, IsTrading = business.IsTrading});
            return Ok(updatedBusiness);
        }
        catch(Exception){
            return BadRequest("Id not found");
        }
    }

    [HttpPut]
    [Route("[action]/{ProductId}")]
    public async Task<IActionResult> UpdatebyProduct(int ProductId, [FromBody] Business business)
    {
        try{
            var updatedBusiness = await _businessRepository.UpdatebyProduct(new Business { ProductId =ProductId, ProductName = business.ProductName, ProductType = business.ProductType, ProductDescription = business.ProductDescription, ProductImage = business.ProductImage, ProductPrice = business.ProductPrice, UnitSize = business.UnitSize, Quantity= business.Quantity});
            return Ok(updatedBusiness);
        }
        catch(Exception){
            return BadRequest("Id not found");
        }
    }
    //Post -> Insert
    [HttpPost]
    [Route("[action]/{Id}")]
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
     [Route("[action]/{Id}")]
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
    //Delete{id} -> Delete
    [HttpDelete("{ProductId}")]
    public IActionResult Delete(long ProductId)
    {
        try
        {
            _businessRepository.Delete(ProductId);
            return Ok($"Business at {ProductId} is deleted");
        }
        catch(Exception)
        {
            return BadRequest("Id is not valid");
        }
    }
}