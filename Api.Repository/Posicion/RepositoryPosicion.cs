using Api.Abstractions.IRepository;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Dapper;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Posicion
{
    public class RepositoryPosicion:IRepositoryPosicion
    {

        #region VARIABLES Y CONSTRUCTOR
            private readonly IConfiguration _configuration;
            private readonly string _connectionString;
            private readonly string _rutaServidor;
            public RepositoryPosicion(IConfiguration configuration) {
                _configuration = configuration;
                _connectionString = configuration.GetConnectionString("CadenaSQL");
                _rutaServidor = configuration.GetSection("Configuracion").GetSection("RutaServidor").Value;
            }
        #endregion
        

        #region CREATED POSITION
            public async Task<ResultDto<int>> CrearPosicion([FromForm]PosicionDto posiciones)
        {
            ResultDto<int> result = new ResultDto<int>();
            string rutaDocumento = Path.Combine(_rutaServidor, posiciones.imgArchivo.FileName);

            try
            {   
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@intIdPosicion", posiciones.intIdPosicion=0);
                parameters.Add("@strPosicion", posiciones.strPosicion);
                parameters.Add("@strCodSucursal", posiciones.strCodSucursal);
                parameters.Add("@strDescripcion", posiciones.strDescripcion);
                parameters.Add("@strUsuario", posiciones.strUsuario);
                parameters.Add("@imgCodigoBarra",rutaDocumento);
                parameters.Add("@intIdTipoPosicion",posiciones.intIdTipoPosicion);
                parameters.Add("@decCapacidad",posiciones.decCapacidad);

                using (var cn=new SqlConnection(_connectionString))
                {
                    int intIdPosicion = await cn.QuerySingleAsync<int>("[Layout].[Sp_Create_Update_Posicion]", parameters,commandType:CommandType.StoredProcedure);
                    result.Message = intIdPosicion > 0 ? "SE CREO CORRECTAMENTE" : "NO SE CREO CORRECTAMENTE";
                    result.Item = intIdPosicion;
                    result.IsSuccess= intIdPosicion > 0 ? true : false;

                    if (intIdPosicion > 0)
                    {
                        using (FileStream newfile = File.Create(rutaDocumento))
                        {
                            posiciones.imgArchivo.CopyTo(newfile);
                            newfile.Flush();
                        }
                    }

                    return result;

                }
            }
            catch (Exception ex) { 
            
                result.MessageException=ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }
        
        #endregion


        #region UPDATE POSITION
            public async Task<ResultDto<int>> UpdatePosicion([FromForm]PosicionDto posiciones)
        {
            ResultDto<int> result = new ResultDto<int>();
            string rutaDocumento = Path.Combine(_rutaServidor, posiciones.imgArchivo.FileName);
 

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@intIdPosicion", posiciones.intIdPosicion);
                parameters.Add("@strPosicion", posiciones.strPosicion);
                parameters.Add("@strCodSucursal", posiciones.strCodSucursal);
                parameters.Add("@strDescripcion", posiciones.strDescripcion);
                parameters.Add("@strUsuario", posiciones.strUsuario);
                parameters.Add("@imgCodigoBarra", rutaDocumento);
                parameters.Add("@intIdTipoPosicion",posiciones.intIdTipoPosicion);
                parameters.Add("@decCapacidad",posiciones.decCapacidad);

                using (var cn = new SqlConnection(_connectionString))
                {
                    int intIdPosicion = await cn.QuerySingleAsync<int>("[Layout].[Sp_Create_Update_Posicion]", parameters, commandType: CommandType.StoredProcedure);
                    result.Message = intIdPosicion > 0 ? "SE CREO CORRECTAMENTE" : "NO SE CREO CORRECTAMENTE";
                    result.Item = intIdPosicion;
                    result.IsSuccess = intIdPosicion > 0 ? true : false;

                    if (intIdPosicion > 0)
                    {
                        if (File.Exists(posiciones.imgCodigoBarra))
                        {
                            File.Delete(posiciones.imgCodigoBarra);
                        }

                        using (FileStream newfile = File.Create(rutaDocumento))
                        {
                            posiciones.imgArchivo.CopyTo(newfile);
                            newfile.Flush();
                        }
                    }

                    return result;

                }
            }
            catch (Exception ex)
            {

                result.MessageException = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }
        #endregion
    
       
        #region LISTA TODAS LAS POSICIONES
            public async Task<ResultDto<PosicionDto>> GetAll(int intIdPosicion, string strCodSucursal)
            {
            ResultDto<PosicionDto> result=new ResultDto<PosicionDto>();

                try
                {
                    DynamicParameters paramenter = new DynamicParameters();
                    paramenter.Add("@intIdPosicion", intIdPosicion=0);
                    paramenter.Add("@strCodSucursal", strCodSucursal );

                    using (var cn=new SqlConnection(_connectionString))
                    {
                        var repuesta = await cn.QueryAsync<PosicionDto>("Layout.Sp_List_Search_Posicion", paramenter, commandType: CommandType.StoredProcedure);
                        result.Data = repuesta.ToList();
                        result.IsSuccess = repuesta.ToList().Count() > 0 ? true:false;
                        result.Message = repuesta.ToList().Count() > 0 ? "INFORMACION ENCONTRADA" : "NO SE ENCONTRO LA INFORMACION";
                        return result;
                    }
                }
                catch (Exception ex) {
                    result.MessageException=ex.Message;
                    result.IsSuccess = false;
                    return result;
                }
            }

        #endregion
        

        #region SEARCH POSITION
        public async Task<ResultDto<PosicionDto>> GetSearch(int intIdPosicion, string strCodSucursal)
        {
            ResultDto<PosicionDto> result = new ResultDto<PosicionDto>();

            try
            {
                DynamicParameters paramenter = new DynamicParameters();
                paramenter.Add("@intIdPosicion", intIdPosicion);
                paramenter.Add("@strCodSucursal", strCodSucursal);

                using (var cn = new SqlConnection(_connectionString))
                {
                    var repuesta = await cn.QuerySingleOrDefaultAsync<PosicionDto>("Layout.Sp_List_Search_Posicion", paramenter, commandType: CommandType.StoredProcedure);
                    result.Item = repuesta;
                    result.IsSuccess = repuesta != null ? true:false;
                    result.Message =  repuesta != null ? "INFORMACION ENCONTRADA" : "NO SE ENCONTRO LA INFORMACION";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.MessageException = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }
        
        #endregion
       
        
        #region  PRINT POSITION
        public async Task<ResultDto<byte[]>> ImprimirPosicion(int intIdPosicion)
        {
            ResultDto<PosicionDto> posicion = new ResultDto<PosicionDto>();
            ResultDto<byte[]> resp = new ResultDto<byte[]>();

            posicion = await GetSearch(intIdPosicion, "*");
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", posicion.Item.imgCodigoBarra);

            if (!System.IO.File.Exists(fullPath))
            {
                resp.MessageException = "No existe archivo";
                resp.IsSuccess = false;
                return resp;
            }

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                var image = iTextSharp.text.Image.GetInstance(fullPath);

                // Escalar la imagen al tamaño de la página con márgenes
                image.ScaleToFit(PageSize.A4.Height - 50, PageSize.A4.Width - 50); // Invertimos altura y ancho para alinearlo a lo largo

                // Rotar la imagen para que quede vertical
                image.RotationDegrees = 90; // Rota la imagen 90 grados hacia la derecha

                // Calcular la posición para centrar la imagen en la página después de la rotación
                float xPosition = (PageSize.A4.Width - image.ScaledWidth) / 2;
                float yPosition = (PageSize.A4.Height - image.ScaledHeight) / 2;

                // Establecer la posición absoluta de la imagen
                image.SetAbsolutePosition(xPosition, yPosition);

                // Agregar la imagen al documento
                document.Add(image);

                document.Close();
                MemoryStream newMs = new MemoryStream(memoryStream.ToArray());
                resp.Item = newMs.ToArray();
                resp.IsSuccess = true;
                resp.Message = "Se generó correctamente";

                return resp;
            }
        }
        #endregion



        #region Busca o Lista los tipo de posiciones

        public async Task<ResultDto<TipoPosicionDto>> Searh_GetAll_TipoPosicion(int intIdTipoPosicion)
        {
            ResultDto<TipoPosicionDto> result=new ResultDto<TipoPosicionDto>();

            try
            {
                using (var cn=new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@intIdTipoPosicion", intIdTipoPosicion);

                    var response= await cn.QueryAsync<TipoPosicionDto>("Layout.Sp_List_Search_TipoPosicion", dynamicParameters, commandType: CommandType.StoredProcedure);

                    
                    result.Data=response.ToList().Count>0? response.ToList():null;
                    result.Item= response.ToList().Count>0 && intIdTipoPosicion==0? response.ToList().FirstOrDefault():null ;
                    result.IsSuccess = true;
                    result.Message = response.ToList().Count() > 0 ? "INFORMACION ENCONTRADA" : "NO SE ENCONTRO INFORMACION";
                    return result;
                }
            }
            catch (Exception ex) { 
                
                result.MessageException = ex.Message;
                result.IsSuccess= false;
                return result;
            }
        }
        #endregion
    }
}
