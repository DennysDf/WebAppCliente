using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using WebAppCliente.Models;

public static class CsvGenerator
{
    public static byte[] GenerateDevicesCsv(IEnumerable<DeviceViewModel> devices)
    {
        using var memoryStream = new MemoryStream();

        // Configuração do CSV
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, config))
        {
            // Mapeamento customizado (opcional)
            csv.Context.RegisterClassMap<DeviceViewModelMap>();

            csv.WriteRecords(devices);
        }

        return memoryStream.ToArray();
    }
}

// Classe para mapeamento explícito das colunas (opcional)
public sealed class DeviceViewModelMap : ClassMap<DeviceViewModel>
{
    public DeviceViewModelMap()
    {
        Map(m => m.Id).Name("ID").Index(0);
        Map(m => m.Name).Name("NOME").Index(1);
    }
}
