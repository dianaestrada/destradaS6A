using destradaS6A.Models;
using System.Text;
using System.Text.Json;

namespace destradaS6A.Views;

public partial class vActElim : ContentPage
{
    public vActElim( Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
		txtNombre.Text = datos.nombre.ToString();
		txtApellido.Text = datos.apellido.ToString();
		txtEdad.Text = datos.edad.ToString();
	}
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (var client = new HttpClient())
            {
                string url = $"http://192.168.100.226/uisraelws/estudiante.php?codigo={txtCodigo.Text}";

                var student = new
                {
                    codigo = txtCodigo.Text,
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    edad = txtEdad.Text
                };

                string jsonData = JsonSerializer.Serialize(student);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Actualización", "Registro actualizado correctamente", "Cerrar");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("Error", $"No se pudo actualizar el registro", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Cerrar");
        }
    }
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        { 
            using (var client = new HttpClient())
            {
                string url = $"http://192.168.100.226/uisraelws/estudiante.php?codigo={txtCodigo.Text}";

                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Eliminación", "Estudiante eliminado correctamente", "Cerrar");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el registro", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}