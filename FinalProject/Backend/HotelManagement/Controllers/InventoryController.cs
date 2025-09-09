using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet("GetAllInventories")]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllItemsAsync());

    [HttpGet("GetInventoryById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetItemByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost("AddInventory")]
    public async Task<IActionResult> Add(AddInventoryItemDto dto)
    {
        var item = await _service.AddItemAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = item.InventoryItemId }, item);
    }

    [HttpPut("UpdateInventory")]
    public async Task<IActionResult> Update(UpdateInventoryItemDto dto)
    {
        var item = await _service.UpdateItemQuantityAsync(dto);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpDelete("DeleteInventory/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteItemAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> LowStock() =>
        Ok(await _service.GetLowStockItemsAsync());
}
