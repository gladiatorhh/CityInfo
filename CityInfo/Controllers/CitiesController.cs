using AutoMapper;
using CityInfo.Models;
using CityInfo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers;

[ApiController]
[Authorize]
[Route("/api/cities")]
public class CitiesController : ControllerBase
{
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _cityInfoRepository = cityInfoRepository;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDtoWithoutPointOfInterest>>> GetCities()
    {
        var cities = await _cityInfoRepository.GetCitiesAsync();

        return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointOfInterest>>(cities));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
    {
        var city = await _cityInfoRepository.GetCityByIdAsync(id, includePointsOfInterest);

        if (city is null)
            return NotFound();

        if (includePointsOfInterest)
        {
            return Ok(_mapper.Map<CityDto>(city));
        }

        return Ok(_mapper.Map<CityDtoWithoutPointOfInterest>(city));
    }
}
