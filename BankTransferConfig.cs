using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Diagnostics.SymbolStore;

namespace Modul8_103022300101
{
    public class Transfer
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }

        public Transfer() { }

        public Transfer(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    public class confrim
    {
        public string en { get; set; }
        public string id { get; set; }

        public confrim() { }

        public confrim(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }
    public class BankTransferConfigWrapper
    {
        public static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "bank_transfer_config");

        [JsonPropertyName("lang")]
        public string lang { get; set; }

        [JsonPropertyName("methods")]
        public List<string> method { get; set; }

        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; }

        [JsonPropertyName("confirm")]
        public confrim confrim { get; set; }

        public BankTransferConfigWrapper() { }

        public BankTransferConfigWrapper(string lang, Transfer transfer, List<string> method, confrim confrim)
        {
            this.lang = lang;
            this.Transfer = transfer;
            this.method = new List<string>();
            this.confrim = new confrim();
        }
    }

    class BankTransferConfig()
    {
        BankTransferConfigWrapper bank = new BankTransferConfigWrapper();

        public const String filePath = @"bank_transfer_config.json";

        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            } catch (Exception e) 
            {
                setDefault();
                Console.WriteLine("Terjadi kesalahan: " + e.Message);
            }
        }

        private BankTransferConfigWrapper ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            BankTransferConfigWrapper = JsonSerializer.Deserialize<BankTransferConfigWrapper>(configJsonData);
            return BankTransferConfigWrapper;
        }

        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            String jsonStgring = JsonSerializer.Serialize(BankTransferConfigWrapper, options);
            File.WriteAllText(filePath, jsonStgring);
        }

        private void setDefault()
        {
            bank.lang = "en";
            bank.Transfer.threshold = 25000000;
            bank.Transfer.low_fee = 6500;
            bank.Transfer.high_fee = 15000;
            bank.confrim.id = "ya";
            bank.confrim.en = "yes";
        }
    }
}
