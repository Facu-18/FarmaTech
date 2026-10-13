using System;
using System.Collections.Generic;
using System.Text;

namespace FarmaTech.Servicios.ServiciosHTTP
{
    public interface IHttpService
    {
        Task<HttpResp<TResp?>> DeleteAsync<TResp>(string url);
        Task<HttpResp<T?>> GetAsync<T>(string url);
        Task<HttpResp<TResp?>> PostAsync<T, TResp>(string url, T DTO);
        Task<HttpResp<TResp?>> PutAsync<T, TResp>(string url, T DTO);
        void EstablecerToken(string? token);
    }
}
