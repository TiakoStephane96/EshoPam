using EshoPam.Repository;
using EshoPam.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EshoPam.WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryRepository categoryRepository;

        public CategoriesController()
        {
            categoryRepository = new CategoryRepository();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var categories = categoryRepository.Get(id);
            if (categories == null)
                return NotFound();

            return Ok(new CategoryModel(categories));
        }

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var categories = categoryRepository.Get(name);
            if (categories == null)
                return NotFound();

            return Ok(new CategoryModel(categories));
        }

        [HttpGet]
        public IHttpActionResult Find(string value)
        {
            var searchValue = value?.ToLower() ?? string.Empty;

            var categories = categoryRepository.Find
                (
                    x =>
                    x.Name.ToLower().Contains(searchValue)
                ) ;

            return Ok(categories.Select( x => new CategoryModel(x)).ToArray());
        }

        [HttpPost]
        public IHttpActionResult Post(CategoryModel categoryModel)
        {
            try
            {
                if (categoryModel == null)
                    return BadRequest();

                var category = new Category
                    (
                        0,
                        categoryModel.Name,
                        categoryModel.UserId
                    );
                category = categoryRepository.Add(category);
                return Ok(new CategoryModel(category));
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IHttpActionResult Put(CategoryModel categoryModel)
        {
            try
            {
                if (categoryModel == null)
                    return BadRequest();

                var category = new Category
                    (
                        categoryModel.Id,
                        categoryModel.Name,
                        categoryModel.UserId
                    );
                category = categoryRepository.Set(category);
                return Ok(new CategoryModel(category));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public IHttpActionResult Deete(int id)
        {
           var category = categoryRepository.Delete(id);
            return Ok(new CategoryModel(category));
        }
    }
}
