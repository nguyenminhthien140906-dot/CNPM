namespace CollabSphere.Application
{
    public class SampleValidator
    {
        public static bool ValidateCreateDto(CreateSampleRequestDto dto, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                errorMessage = "Tên không được để trống.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
