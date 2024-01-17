using AutoMapper;
using CityInfo.Entities;
using CityInfo.Models;
using CityInfo.Repositories;
using CityInfo.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers;

[Route("api/cities/{cityId}/pointOfInterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly IMailService _mailService;
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService,
        CitiesDataStore dataStore, ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _mailService = mailService ?? throw new ArgumentNullException();
        _cityInfoRepository = cityInfoRepository;
        _mapper = mapper;
    }

    #region Get

    [HttpGet]
    public async Task<ActionResult<List<PointOfInterestDto>>> CityPointsOfInterest(int cityId)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        IEnumerable<PointOfInterest> pointsOfInterest = await _cityInfoRepository.GetPointsOfInterestByCityAsync(cityId);

        if (pointsOfInterest == null)
            return NotFound();

        return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
    }

    [HttpGet("{id}", Name = nameof(CityPointOfInterest))]
    public async Task<ActionResult<PointOfInterestDto>> CityPointOfInterest(int cityId, int id)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        PointOfInterest? pointOfInterest = await _cityInfoRepository.GetPointOfInterestByIdAsync(cityId, id);

        if (pointOfInterest == null)
            return NotFound();

        return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
    }

    #endregion

    #region Post

    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>> CreatePoiontOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        var createdPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);

        await _cityInfoRepository.AddPointOfInterestToCity(cityId, createdPointOfInterest);

        await _cityInfoRepository.SaveChangesAsync();

        var pointOfInterestWithDto = _mapper.Map<PointOfInterestDto>(createdPointOfInterest);

        return CreatedAtRoute(nameof(CityPointOfInterest), new
        {
            cityId = cityId,
            id = pointOfInterestWithDto.Id,
        }, pointOfInterestWithDto);
    }

    #endregion

    #region Put

    [HttpPut("{pointId}")]
    public async Task<IActionResult> UpdatePointOfInterest(int cityId, int pointId, PointOfInterestEditDto pointOfInterest)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        var pointOfInterestFromDb = await _cityInfoRepository.GetPointOfInterestByIdAsync(cityId, pointId);

        if (pointOfInterestFromDb == null)
            return NotFound();

        _mapper.Map(pointOfInterest, pointOfInterestFromDb);

        await _cityInfoRepository.SaveChangesAsync();

        return NoContent();
    }

    #endregion

    #region Patch

    [HttpPatch("{pointOfInterestId}")]
    public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestEditDto> pointOfInterestJsonDocument)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        var pointOfInterestFromDb = await _cityInfoRepository.GetPointOfInterestByIdAsync(cityId, pointOfInterestId);

        if (pointOfInterestFromDb == null)
            return NotFound();

        var pointByEditDto = _mapper.Map<PointOfInterestEditDto>(pointOfInterestFromDb);

        pointOfInterestJsonDocument.ApplyTo(pointByEditDto, ModelState);

        if (!ModelState.IsValid)
            return BadRequest();

        if (!TryValidateModel(pointByEditDto))
            return BadRequest();

        await _cityInfoRepository.SaveChangesAsync();

        return NoContent();
    }

    #endregion

    #region Delete

    [HttpDelete("{pointOfInterestId}")]
    public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        if (!await _cityInfoRepository.CheckCityExistByCityIdAsync(cityId))
            return NotFound();

        PointOfInterest? pointOfInterest = await _cityInfoRepository.GetPointOfInterestByIdAsync(cityId, pointOfInterestId);

        if (pointOfInterest == null)
            return NotFound();

        _cityInfoRepository.DeletePointOfInterestById(pointOfInterest);
        await _cityInfoRepository.SaveChangesAsync();

        _mailService.SendMail("Point of interest deleted", $"Point of interest with Id {pointOfInterest.Id} and name {pointOfInterest.Name} deleted");

        return NoContent();
    }

    #endregion
}
