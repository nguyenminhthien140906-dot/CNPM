namespace CollabSphere.Application
{
    public class SampleService : ISampleService
    {
        public async Task<SampleDto?> GetByIdAsync(int id)
        {
            await Task.Delay(10); // Ví dụ xử lý bất đồng bộ
            return new SampleDto { Id = id, Name = "Sample Item", Description = "Sample Description" };
        }

        public async Task<IEnumerable<SampleDto>> GetAllAsync()
        {
            await Task.Delay(10);
            return new List<SampleDto>
            {
                new SampleDto { Id = 1, Name = "Item 1", Description = "Description 1" },
                new SampleDto { Id = 2, Name = "Item 2", Description = "Description 2" }
            };
        }

        public async Task<SampleDto> CreateAsync(CreateSampleRequestDto dto)
        {
            await Task.Delay(10);
            return new SampleDto { Id = 1, Name = dto.Name, Description = dto.Description };
        }
    }
}
