

namespace MaterialesIza.Controllers
{
    using MaterialesIza.Data.Entities;
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    public class ProductTypesController : Controller
    {
        private readonly IProductTypeRepository productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        // GET: ProductTypes
        public IActionResult Index()
        {
            return View(this.productTypeRepository.GetAll());
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await this.productTypeRepository.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await this.productTypeRepository.CreateAsync(productType);
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await this.productTypeRepository.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.productTypeRepository.UpdateAsync(productType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productTypeRepository.ExistAsync(productType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await this.productTypeRepository.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            await this.productTypeRepository.DeleteAsync(productType);
            return RedirectToAction(nameof(Index));
        }



    }
}
