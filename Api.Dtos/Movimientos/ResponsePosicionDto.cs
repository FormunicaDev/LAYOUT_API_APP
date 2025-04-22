namespace Api.Dtos.Movimientos;

public class ResponsePosicionDto
{
    public int intIdPosicion { get; set; }
    public string ?strPosicion{ get; set; }
    public string ?strDescripcion { get; set; }
    public string strDescripcionCompleta => (strPosicion ?? "") + " - " + (strDescripcion ?? "");
}
