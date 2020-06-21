﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiendita.Models;

namespace Tiendita
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        public static void MenuPrincipal()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1) Menú de Productos");
            Console.WriteLine("2) Menú de Ventas");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    MenuProductos();
                    break;
                case "2":
                    MenuVentas();
                    break;
                case "0": return;
            }
            MenuPrincipal();


        }

        static void OpcionesMenu(string nombre)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1) Buscar " + nombre);
            Console.WriteLine("2) Crear " + nombre);
            Console.WriteLine("3) Actualizar " + nombre);
            Console.WriteLine("4) Eliminar " + nombre);
            Console.WriteLine("0) Volver al menú principal");
        }

        // Productos

        public static void MenuProductos()
        {
            OpcionesMenu("producto");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarProductos();
                    break;
                case "2":
                    CrearProducto();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;
                case "0":
                    MenuPrincipal();
                    break;

                default: 
                    MenuPrincipal();
                    break;
            }
            MenuProductos();
        }

        public static void BuscarProductos()
        {
            Console.WriteLine("Buscar productos");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0);
             
                    foreach (Producto producto in productos)
                    {
                        Console.WriteLine(producto);
                    }
                
            }
        }

        public static void CrearProducto()
        {
            Console.WriteLine("Crear producto");
            Producto producto = new Producto();
            producto = LlenarProducto(producto);

            using (TienditaContext context = new TienditaContext())
            {
                context.Add(producto);
                context.SaveChanges();
                Console.WriteLine("Producto creado");
            }
        }

        public static Producto LlenarProducto(Producto producto)
        {
            Console.Write("Nombre: ");
            producto.Nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            producto.Descripcion = Console.ReadLine();
            Console.Write("Precio: ");
            producto.Precio = decimal.Parse(Console.ReadLine());
            Console.Write("Costo: ");
            producto.Costo = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            producto.Cantidad = decimal.Parse(Console.ReadLine());
            Console.Write("Tamaño: ");
            producto.Tamano = Console.ReadLine();

            return producto;
        }

        public static Producto SelecionarProducto()
        {
            BuscarProductos();
            Console.Write("Seleciona el código de producto: ");
            uint id = 0;
            try
            {
                id = uint.Parse(Console.ReadLine());
                
            }
            catch
            {
                Console.WriteLine("Valor inválido");
            }
            using (TienditaContext context = new TienditaContext())
            {
                Producto producto = context.Productos.Find(id);
                if (producto == null)
                {
                    SelecionarProducto();
                }
                return producto;
            }
        }

           

        public static void ActualizarProducto()
        {
            Console.WriteLine("Actualizar producto");
            Producto producto = SelecionarProducto();
            producto = LlenarProducto(producto);
            using (TienditaContext context = new TienditaContext())
            {
                context.Update(producto);
                context.SaveChanges();
                Console.WriteLine("Producto actualizado");
            }
        }

        public static void EliminarProducto()
        {
            Console.WriteLine("Eliminar producto");
            Producto producto = SelecionarProducto();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(producto);
                context.SaveChanges();
                Console.WriteLine("Producto eliminado");
            }
        }


        // Ventas 

        public static void MenuVentas()
        {
            OpcionesMenu("venta");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarVentas();
                    break;
                case "2":
                    CrearVenta();
                    break;
                case "3":
                    
                    break;
                case "4":
                    EliminarVenta();
                    break;
                case "0":
                    MenuPrincipal();
                    break;

                default:
                    MenuVentas();
                    break;
            }

            MenuVentas();
        }

        public static void CrearVenta()
        {
            Console.WriteLine("Crear Venta");
            Venta venta = new Venta();
            venta.Fecha = new DateTime();
            Console.WriteLine("Nombre de Cliente");
            venta.Cliente = Console.ReadLine();
           
            
            // Lista de productos en la venta
            List<Producto> productos = new List<Producto>();
            Console.WriteLine("Agregar productos a la venta");
            var agregar = 0;
            do
            {
            agregar = 0;
            Producto producto = new Producto();
            producto = SelecionarProducto();
            if(producto != null)
                {
                    productos.Add(producto);
                }
                while ( agregar != 2 && agregar !=1)
                {
                    Console.WriteLine("¿Deseas agregar otro producto?");
                    Console.WriteLine("1) Sí");
                    Console.WriteLine("2) No");
                    Console.WriteLine("Ingresa el número de la respuesta");

                    try
                    {
                        agregar = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ingresaste una letra");
                    }
                };
                
                

            } while (agregar == 1);  ;
          ;
           
            
            List<Detalle> detalles = new List<Detalle>();
            foreach (Producto p in productos)
            {
                var detalle = new Detalle();
                detalle.ProductoId = p.Id;
                detalle.Producto = p;
                detalle.Subtotal = detalle.Subtotal + p.Precio;

                if(detalle != null)
                {
                    detalles.Add(detalle);
                }
            
            }
           
            venta.Detalles = detalles;
            venta.Total = detalles.Sum(x => x.Subtotal);
            using (TienditaContext context = new TienditaContext())
            {

                foreach (Detalle d in venta.Detalles) {
                    context.Detalles.Attach(d);
                }
                
                context.Add(venta);
                context.SaveChanges();
                Console.WriteLine("Venta creada");
            }
        }


        public static void EliminarVenta()
        {

            Console.WriteLine("Eliminar venta");
            Venta venta= SelecionarVenta();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(venta);
                context.SaveChanges();
                Console.WriteLine("Venta eliminada");
            }

            
        }


        public static Venta SelecionarVenta()
        {
            BuscarVentas();
            Console.Write("Seleciona el código de la venta: ");
            uint id = 0;
            try
            {

                id = uint.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Valor inválido");
            }

           
            using (TienditaContext context = new TienditaContext())
            {
                Venta venta= context.Ventas.Find(id);
                if (venta == null)
                {
                    SelecionarVenta();
                }
                return venta;
            }
        }


        public static void BuscarVentas()
        {
            Console.WriteLine("Buscar ventas");
            Console.Write("Ingresa nombre del cliente: ");
            string buscar = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Venta> ventas= context.Ventas.Where(p => p.Cliente.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0).Include(p => p.Detalles);
                
                 foreach (Venta v in ventas)
                    {
                    Console.WriteLine("------------Venta----------");
                        Console.WriteLine(v);
                    Console.WriteLine("          Detalles       ");

                    foreach ( Detalle d in v.Detalles)
                    {
                        Console.WriteLine(d);
                    }
                    }
            }
        }

    }
}
