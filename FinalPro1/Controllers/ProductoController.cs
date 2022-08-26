﻿using FinalPro1.Models;
using Microsoft.AspNetCore.Mvc;
using FinalPro1.Repository;
using FinalPro1.Controllers.DTOS;

namespace FinalPro1.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {


            return ProductoHandler.GetProductos();

        }

        [HttpDelete(Name = "BorrarProducto")]

        public void DeleteProducto([FromBody] int Id)
        {
            return ProductoHandler.DeleteProducto(Id);

        }

        [HttpPut(Name = "ModificarProducto")]
        public void ModificarProducto([FromBody]PutProducto producto)
        {

            return ProductoHandler.ModificarProducto(producto);

        }

        [HttpPost(Name = "CrearProducto")]

        public void CrearProducto([FromBody] PostProducto producto)
        {


            return ProductoHandler.CrearProducto(producto);

        }
    }
}
