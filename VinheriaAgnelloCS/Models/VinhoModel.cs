namespace VinheriaAgnelloCS.Models;

public class VinhoModel
{
    
    public int id { get; set; }
    public string nome { get; set; }
    
    public string descricao{ get; set; }
    
    public string imagem{ get; set; }
    
    public string marca{ get; set; }
    
    public string categoria{ get; set; }
    
    public double preco { get; set; }
}