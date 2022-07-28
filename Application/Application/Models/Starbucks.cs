using static Mernis.KPSPublicSoapClient;

namespace Application.Models
{
    public class Starbucks
    {
        public int Id { get; set; }

        public string? Ad { get; set; }

        public string? Soyad { get; set; }

        public string? TcKimlikNo { get; set; }

        public DateTime? DogumTarihi { get; set; }
        public  Task<bool> TcKimlikDogrula(Starbucks starbucks)
        {
            bool dogrulamaSonucu = false;
            try
            {
                var mernisClient = new Mernis.KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
                var tcKimlikDogrulamaServisResponse = mernisClient.TCKimlikNoDogrulaAsync(long.Parse(starbucks.TcKimlikNo), starbucks.Ad, starbucks.Soyad, starbucks.DogumTarihi.Value.Year).GetAwaiter().GetResult();
                dogrulamaSonucu = tcKimlikDogrulamaServisResponse.Body.TCKimlikNoDogrulaResult;
            }
            catch (Exception ex)
            {
                dogrulamaSonucu = false;
            }
            return Task.FromResult(dogrulamaSonucu);
        }

        
    }
}
