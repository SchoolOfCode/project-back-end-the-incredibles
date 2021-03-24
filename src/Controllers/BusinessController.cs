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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        try{
        var returnedBusiness = await _businessRepository.Get(id);
        return Ok(returnedBusiness);
        }
        catch (Exception){
        return BadRequest("Id not found");
        }
    }
    //Put{id} -> Update(business)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Business business)
    {
        try{
            var updatedBusiness = await _businessRepository.Update(new Business {Id = id, BusinessName = business.BusinessName, PrimaryContact = business.PrimaryContact, AddrBuildingNumber = business.AddrBuildingNumber, AddrBuildingName = business.AddrBuildingName, AddrStreet = business.AddrStreet, AddrCity = business.AddrCity, AddrCounty = business.AddrCounty, AddrPostcode = business.AddrPostcode, TelephoneNumber = business.TelephoneNumber, TwitterHandle = business.TwitterHandle, SocialmediaLink = business.SocialmediaLink, BusinessImage = business.BusinessImage, IsTrading = business.IsTrading});
            return Ok(updatedBusiness);
        }
        catch(Exception){
            return BadRequest("Id not found");
        }
    }
    //Post -> Insert
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Business business)
    {
        try 
        {
            var insertedBusiness = await _businessRepository.Insert(business);
            return Created($"/businesses/{insertedBusiness.Id}", insertedBusiness);
        }
        catch(Exception)
        {
            return BadRequest("Business entered is not valid");
        }
    }
    //Delete{id} -> Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _businessRepository.Delete(id);
            return Ok($"Business at {id} is deleted");
        }
        catch(Exception)
        {
            return BadRequest("Id is not valid");
        }
    }
}