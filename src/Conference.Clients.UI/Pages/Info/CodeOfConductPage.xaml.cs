using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Conference.Clients.UI
{
    public partial class CodeOfConductPage : ContentPage
    {
        public static string Conduct = "Queremos que LaComarca sea la mejor comunidad en la que hayas participado. Además del excelente contenido, la capacitación técnica, las oportunidades de creación de redes y los eventos divertidos, queremos asegurarnos de que la conferencia sea un entorno seguro y productivo para todos los participantes." +
        "\n\nComo tal, estamos dedicados a brindar una experiencia de conferencia sin hostigamiento para todos, independientemente de su sexo, orientación sexual, discapacidad, apariencia física, tamaño corporal, raza o religión. No toleramos el hostigamiento de los participantes de la conferencia en ningún El lenguaje sexual y las imágenes no son apropiados para ningún lugar de la conferencia, incluidas las charlas. Se puede pedir a los participantes de la conferencia que violen estas reglas que se retiren (sin reembolso) a discreción de los organizadores de la conferencia." +
        "\n\nEl hostigamiento incluye comentarios verbales ofensivos relacionados con el género, la orientación sexual, la discapacidad, la apariencia física, el tamaño corporal, la raza, la religión, las imágenes sexuales en espacios públicos, la intimidación deliberada, el acecho, el seguimiento, el hostigamiento de fotografías o grabaciones, la interrupción sostenida de las conversaciones. u otros eventos, contacto físico inapropiado y atención sexual no deseada. Se espera que los participantes a los que se les pida que detengan cualquier conducta de acoso obedezcan de inmediato." +
        "\n\nLos expositores en la sala de exposiciones, patrocinadores o puestos de vendedores o actividades similares también están sujetos a este código de conducta. En particular, los expositores no deben usar imágenes, actividades u otro material sexualizado. El personal del stand (incluidos los voluntarios) no debe use ropa / uniformes / disfraces sexualizados, o de lo contrario, cree un ambiente sexualizado." +
        "\n\nSi está siendo acosado, note que alguien más está siendo acosado, o si tiene alguna otra inquietud, comuníquese con un miembro del personal de la conferencia inmediatamente. El personal de la conferencia puede identificarse con camisetas y distintivos especiales." +
        "\n\nEl personal de la conferencia estará encantado de ayudar a los participantes a ponerse en contacto con la seguridad del hotel / lugar de reunión o con la policía local, proporcionar escoltas o ayudar a las personas que sufren acoso a sentirse seguros durante la conferencia." +
        "\n\nDamos las gracias a nuestros asistentes, oradores y expositores por su ayuda para mantener el evento acogedor, respetuoso y amigable con todos los participantes para que todos podamos disfrutar de una gran conferencia."; 
        public CodeOfConductPage()
        {
            InitializeComponent();

            CodeOfConductText.Text = Conduct;
        }
    }
}

