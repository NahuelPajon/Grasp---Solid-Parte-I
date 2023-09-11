﻿//-------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections;
using System.Linq;
using Full_GRASP_And_SOLID.Library;

namespace Full_GRASP_And_SOLID
{
    public class Program
    {
        private static ArrayList productCatalog = new ArrayList();

        private static ArrayList equipmentCatalog = new ArrayList();

        public static void Main(string[] args)
        {
            PopulateCatalogs();

            Recipe TheRecipe = new Recipe();
            TheRecipe.FinalProduct = GetProduct("Café con leche");
            TheRecipe.AddStep(new Step(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120));
            TheRecipe.AddStep(new Step(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60));
            TheRecipe.PrintRecipe();
            GetProductionCost(TheRecipe);
        }

        private static void PopulateCatalogs()
        {
            AddProductToCatalog("Café", 100);
            AddProductToCatalog("Leche", 200);
            AddProductToCatalog("Café con leche", 300);

            AddEquipmentToCatalog("Cafetera", 1000);
            AddEquipmentToCatalog("Hervidor", 2000);
        }

        private static void AddProductToCatalog(string description, double unitCost)
        {
            productCatalog.Add(new Product(description, unitCost));
        }

        private static void AddEquipmentToCatalog(string description, double hourlyCost)
        {
            equipmentCatalog.Add(new Equipment(description, hourlyCost));
        }

        private static Product ProductAt(int index)
        {
            return productCatalog[index] as Product;
        }

        private static Equipment EquipmentAt(int index)
        {
            return equipmentCatalog[index] as Equipment;
        }

        private static Product GetProduct(string description)
        {
            var query = from Product product in productCatalog where product.Description == description select product;
            return query.FirstOrDefault();
        }

        private static Equipment GetEquipment(string description)
        {
            var query = from Equipment equipment in equipmentCatalog where equipment.Description == description select equipment;
            return query.FirstOrDefault();
        }

        public static double GetProductionCost(Recipe recipe)
        {
            // Step myStep = new Step(); // Crea una instancia de Step
            Step myStep = new Step(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120);
            Step myStep2 = new Step(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60);
            foreach (Step step in recipe.steps)
            {
                step.GetStepCost(recipe);
            }
            double TotalCost =  myStep.GetStepCost(recipe) + myStep2.GetStepCost(recipe) +
                                myStep.GetStepTime(recipe) + myStep2.GetStepTime(recipe);
            return TotalCost;   
        }

    }
}