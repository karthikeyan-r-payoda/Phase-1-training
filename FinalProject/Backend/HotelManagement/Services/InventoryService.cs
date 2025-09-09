using HotelManagement.context;
using Microsoft.EntityFrameworkCore;

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync()
    {
        try
        {
            return await _context.InventoryItems
                .Select(i => new InventoryItemDto
                {
                    InventoryItemId = i.InventoryItemId,
                    ItemName = i.ItemName,
                    Category = i.Category,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    ReorderLevel = i.ReorderLevel,
                    LastUpdated = i.LastUpdated
                }).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving inventory items: {ex.Message}", ex);
        }
    }

    public async Task<InventoryItemDto?> GetItemByIdAsync(int id)
    {
        try
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null) return null;

            return new InventoryItemDto
            {
                InventoryItemId = item.InventoryItemId,
                ItemName = item.ItemName,
                Category = item.Category,
                Quantity = item.Quantity,
                Unit = item.Unit,
                ReorderLevel = item.ReorderLevel,
                LastUpdated = item.LastUpdated
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving inventory item with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<InventoryItemDto> AddItemAsync(AddInventoryItemDto dto)
    {
        try
        {
            var item = new InventoryItemModel
            {
                ItemName = dto.ItemName,
                Category = dto.Category,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                ReorderLevel = dto.ReorderLevel,
                LastUpdated = DateTime.Now
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            return new InventoryItemDto
            {
                InventoryItemId = item.InventoryItemId,
                ItemName = item.ItemName,
                Category = item.Category,
                Quantity = item.Quantity,
                Unit = item.Unit,
                ReorderLevel = item.ReorderLevel,
                LastUpdated = item.LastUpdated
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding inventory item: {ex.Message}", ex);
        }
    }

    public async Task<InventoryItemDto?> UpdateItemQuantityAsync(UpdateInventoryItemDto dto)
    {
        try
        {
            var item = await _context.InventoryItems.FindAsync(dto.InventoryItemId);
            if (item == null) return null;

            item.Quantity = dto.Quantity;
            item.LastUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return new InventoryItemDto
            {
                InventoryItemId = item.InventoryItemId,
                ItemName = item.ItemName,
                Category = item.Category,
                Quantity = item.Quantity,
                Unit = item.Unit,
                ReorderLevel = item.ReorderLevel,
                LastUpdated = item.LastUpdated
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating quantity for item ID {dto.InventoryItemId}: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        try
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null) return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting inventory item with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<InventoryItemDto>> GetLowStockItemsAsync()
    {
        try
        {
            return await _context.InventoryItems
                .Where(i => i.Quantity <= i.ReorderLevel)
                .Select(i => new InventoryItemDto
                {
                    InventoryItemId = i.InventoryItemId,
                    ItemName = i.ItemName,
                    Category = i.Category,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    ReorderLevel = i.ReorderLevel,
                    LastUpdated = i.LastUpdated
                }).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving low-stock items: {ex.Message}", ex);
        }
    }
}
