namespace CollabSphere.Application
{
    public interface ISampleService
    {
        Task<SampleDto?> GetByIdAsync(int id);
        Task<IEnumerable<SampleDto>> GetAllAsync();
        Task<SampleDto> CreateAsync(CreateSampleRequestDto dto);
    }

    public interface ISampleRepository
    {
        Task<SampleDto?> GetByIdAsync(int id);
        Task<IEnumerable<SampleDto>> GetAllAsync();
        Task AddAsync(SampleDto entity);
    }
}
