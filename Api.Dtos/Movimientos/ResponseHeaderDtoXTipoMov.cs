namespace Api.Dtos.Movimientos;

public class ResponseHeaderDtoXTipoMov
{
   public string intIdMovimientoArticulo{set;get;}
   public string strCodSucursal{set;get;}
   public string strDescripcion {set;get;}
   public string strNumeroDocumento{set;get;} 
   public int intIdTipoMovimiento{set;get;}
   public string strTipoMovimiento{set;get;}
    public string strEstado { set;get;}
   public DateTime datFechaCrea{set;get;}

}
