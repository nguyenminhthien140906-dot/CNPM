
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;

namespace CollabSphere.Frontend.Contexts;

public class AuthContext
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    private const string TokenKey = "collabsphere_token";

    public AuthContext(
        HttpClient httpClient,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public UserInfo? User { get; private set; }

    public string? Token { get; private set; }

    public bool Loading { get; private set; } = true;

    public bool IsAuthenticated =>
        !string.IsNullOrEmpty(Token) && User != null;

    public event Action? OnChange;

    public async Task<AuthResult> LoginAsync(
        string usernameOrEmail,
        string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                new
                {
                    usernameOrEmail,
                    password
                }
            );

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<LoginData>>();

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = result?.Message ??
                        "Tên đăng nhập hoặc mật khẩu không đúng."
                };
            }

            var authData = result?.Data;

            if (string.IsNullOrWhiteSpace(authData?.AccessToken))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Không nhận được Access Token."
                };
            }

            Token = authData.AccessToken;
            User = authData.User;

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                TokenKey,
                Token
            );

            NotifyStateChanged();

            return new AuthResult
            {
                Success = true,
                User = User
            };
        }
        catch (Exception ex)
        {
            return new AuthResult
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            TokenKey
        );

        Token = null;
        User = null;

        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyStateChanged();
    }

    public async Task<UserInfo?> GetCurrentUserAsync(
        string accessToken)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "api/Auth/me"
        );

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                accessToken
            );

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException(
                "Phiên đăng nhập không hợp lệ."
            );
        }

        var result = await response.Content
            .ReadFromJsonAsync<ApiResponse<UserInfo>>();

        return result?.Data;
    }

    public async Task InitializeAsync()
    {
        try
        {
            Token = await _jsRuntime.InvokeAsync<string?>(
                "localStorage.getItem",
                TokenKey
            );

            if (string.IsNullOrWhiteSpace(Token))
            {
                return;
            }

            User = await GetCurrentUserAsync(Token);
        }
        catch
        {
            Token = null;
            User = null;

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.removeItem",
                TokenKey
            );
        }
        finally
        {
            Loading = false;
            NotifyStateChanged();
        }
    }

    public bool HasRole(string role)
    {
        if (User == null)
            return false;

        return string.Equals(
            User.RoleCode,
            role,
            StringComparison.OrdinalIgnoreCase
        );
    }

    public bool HasRole(IEnumerable<string> roles)
    {
        if (User == null)
            return false;

        return roles.Any(role =>
            string.Equals(
                User.RoleCode,
                role,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }

    public void SetUser(UserInfo? user)
    {
        User = user;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}

public class AuthResult
{
    public bool Success { get; set; }

    public string? Message { get; set; }

    public UserInfo? User { get; set; }
}

public class LoginData
{
    public string AccessToken { get; set; } = string.Empty;

    public UserInfo? User { get; set; }
}

public class UserInfo
{
    public ulong UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? RoleCode { get; set; }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string? Message { get; set; }

    public T? Data { get; set; }
}
