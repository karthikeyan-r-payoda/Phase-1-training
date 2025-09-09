public interface IInventoryService
{
    Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync();
    Task<InventoryItemDto?> GetItemByIdAsync(int id);
    Task<InventoryItemDto> AddItemAsync(AddInventoryItemDto dto);
    Task<InventoryItemDto?> UpdateItemQuantityAsync(UpdateInventoryItemDto dto);
    Task<bool> DeleteItemAsync(int id);
    Task<IEnumerable<InventoryItemDto>> GetLowStockItemsAsync();
}
