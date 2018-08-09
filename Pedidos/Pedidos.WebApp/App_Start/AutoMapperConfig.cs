﻿using AutoMapper;
using Pedidos.BO.EntitiesBO;
using Pedidos.DAL.Entities;

namespace Pedidos.WebApp
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Producto, ProductoBO>();
                cfg.CreateMap<ProductoBO, Producto>();
            });
        }
    }
}