namespace IntelligentHabitacion.Useful
{
    public class State
    {
        public string StateAbbreviationToFullNameState(string abbreviation)
        {
            switch (abbreviation.ToUpper())
            {
                case "AC":
                    {
                        return "Acre";
                    }
                case "AL":
                    {
                        return "Alagoas";
                    }
                case "AP":
                    {
                        return "Amapá";
                    }
                case "AM":
                    {
                        return "Amazonas";
                    }
                case "BA":
                    {
                        return "Bahia";
                    }
                case "CE":
                    {
                        return "Ceará";
                    }
                case "ES":
                    {
                        return "Espírito Santo";
                    }
                case "GO":
                    {
                        return "Goiás";
                    }
                case "MA":
                    {
                        return "Maranhão";
                    }
                case "MT":
                    {
                        return "Mato Grosso";
                    }
                case "MS":
                    {
                        return "Mato Grosso do Sul";
                    }
                case "MG":
                    {
                        return "Minas Gerais";
                    }
                case "PA":
                    {
                        return "Pará";
                    }
                case "PB":
                    {
                        return "Paraíba";
                    }
                case "PR":
                    {
                        return "Paraná";
                    }
                case "PE":
                    {
                        return "Pernambuco";
                    }
                case "PI":
                    {
                        return "Piauí";
                    }
                case "RJ":
                    {
                        return "Rio de Janeiro";
                    }
                case "RN":
                    {
                        return "Rio Grande do Norte";
                    }
                case "RS":
                    {
                        return "Rio Grande do Sul";
                    }
                case "RO":
                    {
                        return "Rondônia";
                    }
                case "RR":
                    {
                        return "Roraima";
                    }
                case "SC":
                    {
                        return "Santa Catarina";
                    }
                case "SP":
                    {
                        return "São Paulo";
                    }
                case "SE":
                    {
                        return "Sergipe";
                    }
                case "TO":
                    {
                        return "Tocantins";
                    }
                case "DF":
                    {
                        return "Distrito Federal";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}
