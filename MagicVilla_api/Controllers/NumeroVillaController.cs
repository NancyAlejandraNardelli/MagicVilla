﻿using AutoMapper;
using MagicVilla_api.Datos;
using MagicVilla_api.Modelos;
using MagicVilla_api.Modelos.Dto;
using MagicVilla_api.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio  _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;

        protected ApiResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo,
                                                                            INumeroVillaRepositorio numeroRepo, IMapper mapper)
        {
                _logger = logger;
                _villaRepo = villaRepo;
                _mapper = mapper;
                _response = new ();
                _numeroRepo = numeroRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetNumeroVillas() 
        {
            try
            {
                _logger.LogInformation("Obtener nùmero de villas");
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

               _response.IsExitoso = false;
               _response.ErrorMessages= new List<string>() { ex.ToString()};
            }
            return _response;
            
        }

        [HttpGet("id:int", Name ="GetNumeroVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Error al traer numero Villa con id " + id);
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);
                if (numeroVilla == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso=false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearNumeroVillaDto([FromBody] NumeroVillaCreateDto createDto) {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo )!= null)
                {
                    ModelState.AddModelError("NumeroVillaExiste", "El número de villa ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Obtener(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                modelo.FechaCreacion=DateTime.Now;
                modelo.FechaActualizacion=DateTime.Now;

                await _numeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode=HttpStatusCode.Created;


                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string> { ex.ToString() };
            }
            return _response;
            
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso=false;
                    _response.StatusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo.Equals(id));
                if (numeroVilla == null)
                {
                    _response.IsExitoso=false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                await _numeroRepo.Remover(numeroVilla);
                _response.StatusCode=HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
           
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if(updateDto == null || id != updateDto.VillaNo) 
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _villaRepo.Obtener(v=>v.Id == updateDto.VillaId)==null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe");
                return BadRequest(ModelState);
            }
            
            NumeroVilla modelo= _mapper.Map<NumeroVilla>(updateDto);
           
            await _numeroRepo.Actualizar(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }
    }
}
