using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Wpf.Shaders
{
    public class DissovleEffect : ShaderEffect
    {
        private static readonly PixelShader _pixelShader;

        public static readonly DependencyProperty MainTextureProperty =
            RegisterPixelShaderSamplerProperty(nameof(MainTexture), typeof(DissovleEffect), 0);
        public static readonly DependencyProperty NoiseTextureProperty =
            RegisterPixelShaderSamplerProperty(nameof(NoiseTexture), typeof(DissovleEffect), 1);
        public static readonly DependencyProperty DissolveAmountProperty =
            DependencyProperty.Register(nameof(DissolveAmount), typeof(double), typeof(DissovleEffect), new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)));

        public Brush MainTexture
        {
            get => (Brush)GetValue(MainTextureProperty);
            set => SetValue(MainTextureProperty, value);
        }
        public Brush NoiseTexture
        {
            get => (Brush)GetValue(NoiseTextureProperty);
            set => SetValue(NoiseTextureProperty, value);
        }
        public double DissolveAmount
        {
            get => (double)GetValue(DissolveAmountProperty);
            set => SetValue(DissolveAmountProperty, value);
        }

        static DissovleEffect()
        {
            _pixelShader = new PixelShader();
            var asm = Assembly.GetExecutingAssembly();
            using (Stream stream = asm.GetManifestResourceStream("Wpf.Shaders.DissolveEffect.ps")) {
                _pixelShader.SetStreamSource(stream);
            }
        }

        public DissovleEffect()
        {
            PixelShader = _pixelShader;
            UpdateShaderValue(MainTextureProperty);
            UpdateShaderValue(NoiseTextureProperty);
            UpdateShaderValue(DissolveAmountProperty);
        }
    }
}
