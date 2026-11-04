namespace CollabSphere.Application
{
    public class CreateSampleUseCase
    {
        private readonly ISampleService _sampleService;

        public CreateSampleUseCase(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public async Task<SampleDto> ExecuteAsync(CreateSampleRequestDto request)
        {
            // Thực hiện các luồng nghiệp vụ chính tại đây
            return await _sampleService.CreateAsync(request);
        }
    }
}
