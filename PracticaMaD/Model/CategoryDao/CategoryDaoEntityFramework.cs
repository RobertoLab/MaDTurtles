﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : 
        GenericDaoEntityFramework<Category, Int32>, ICategoryDao
    {
        public string GetCategoryValue(long categoryId)
        {
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from cat in categories
                 where cat.categoryId == categoryId
                 select cat.category).Single();

            return result;
        }
    }
}
