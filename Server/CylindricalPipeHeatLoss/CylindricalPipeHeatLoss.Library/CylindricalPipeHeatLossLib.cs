﻿using CylindricalPipeHeatLoss.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CylindricalPipeHeatLoss.Library
{
    public class CylindricalPipeHeatLossLib
    {
        private const int EXTRA_TEMPS_COUNT = 3;

        private const double C0 = 5.67;
        
        private readonly double _innerPipeRadius;
        
        private readonly double _a1;
        
        private double _a2;
        
        private readonly double _e;
        
        private readonly List<PipeLayer> _pipeLayers;
        
        private readonly double _innerTemp;
        
        private readonly double _outterTemp;

        private readonly double _precision;

        private readonly PipeOrientation _pipeOrientation;

        private readonly double _pipeLength;

        private List<double> _temps = [];
        
        private List<double> _qls = [];

        private List<double> _radiuses = [];

        private double _criticalDiam;

        private double InnerQl { get; set; }

        private double OutterQl { get; set; }

        public double Q => _qls[0] * _pipeLength;

        public CylindricalPipeHeatLossLib(
        double innerPipeRadius,
        double a1,
        double e,
        List<PipeLayer> pipeLayers,
        double innerTemp,
        double outterTemp,
        double precision,
        PipeOrientation pipeOrientation = PipeOrientation.Horizontal,
        double pipeLength = 1)
        {
            _innerPipeRadius = innerPipeRadius;
            _a1 = a1;
            _e = e;
            _pipeLayers = pipeLayers;
            _innerTemp = innerTemp;
            _outterTemp = outterTemp;
            _precision = precision;
            _pipeOrientation = pipeOrientation;
            _pipeLength = pipeLength;

            _radiuses.Add(innerPipeRadius);

            CalcTempsAndQls();
            CalcCriticalDiam();
        }

        public ReportModel GetReport() => new()
            {
                Q = RoundToPrecision(Q),
                ql = RoundToPrecision(_qls[0]),
                PipeLayers = _pipeLayers,
                PipeLength = _pipeLength,
                Temperatures = _temps.Select(RoundToPrecision).ToList(),
                Radiuses = _radiuses.Select(RoundToPrecision).ToList(),
                a2 = RoundToPrecision(_a2),
                a1 = RoundToPrecision(_a1),
                InnerTemp = _innerTemp,
                OutterTemp = _outterTemp,
                e = _e,
                InnerQl = RoundToPrecision(InnerQl),
                OutterQl = RoundToPrecision(OutterQl),
                CriticalDiameter = RoundToPrecision(_criticalDiam)
            };

        private double RoundToPrecision(double number) => Round(number / _precision) * _precision;

        private void CalcCriticalDiam() => _criticalDiam = 2.0 * _pipeLayers[^1].ThermalConductivityCoeff / _a2;

        private void CalcTempsAndQls()
        {
            SetInitialTemps();
            CalcRadiuses();

            while (!AreAllItemsEqual(_qls, _precision))
            {
                _qls.Clear();
                _a2 = CalcA2(_temps[^1]);   

                var innerRl = 1 / (_a1 * _innerPipeRadius);
                InnerQl = PI * Abs(_innerTemp - _temps[0]) / innerRl;
                _qls.Add(InnerQl);

                for (var i = 0; i < _pipeLayers.Count; i++)
                {
                    _pipeLayers[i].ThermalConductivityCoeff = _pipeLayers[i].GetThermalConductivityCoeff((_temps[i] + _temps[i + 1]) / 2);
                    _pipeLayers[i].Ql = 2 * PI * _pipeLayers[i].ThermalConductivityCoeff / Log(_radiuses[i + 1] / _radiuses[i]) * (_temps[i] - _temps[i + 1]);
                    _qls.Add(_pipeLayers[i].Ql);
                }

                var outterRl = 1 / (_a2 * _radiuses[^1]);
                OutterQl = PI * Abs(_temps[^1] - _outterTemp) / outterRl;
                _qls.Add(OutterQl);

                var avgQl = _qls.Average();
                for (var i = 0; i < _qls.Count; i++)
                    CalibrateTemps(avgQl, _qls[i], i - 1, i);
            }
        }

        private bool AreAllItemsEqual(List<double> qls, double precision)
        {
            if (qls.Count == 0)
                return false;

            var fVal = qls[0];

            foreach (var val in qls)
            {
                if (Abs(val - fVal) >= precision)
                    return false;
            }

            return true;
        }

        private void SetInitialTemps()
        {
            var tempSum = _innerTemp + _outterTemp;
            var tempsCount = _pipeLayers.Count + EXTRA_TEMPS_COUNT;

            _temps.Add(tempSum / tempsCount * (tempsCount - 1));

            for (var i = 0; i < _pipeLayers.Count; i++)
                _temps.Add(tempSum / tempsCount * (tempsCount - i - 2));
        }

        private void CalcRadiuses()
        {
            for (var i = 0; i < _pipeLayers.Count; i++)
                _radiuses.Add(_radiuses[i] + _pipeLayers[i].Width);
        }

        private double CalcA2(double outterSideTemp)
        {
            var k = _pipeOrientation switch
            {
                PipeOrientation.Horizontal => 3.3,
                _ => 2.4
            };

            return k * Pow(outterSideTemp - _outterTemp, 1 / 4) + _e * C0 * (Pow(CelciasToKelvin(outterSideTemp) / 100, 4) - Pow(CelciasToKelvin(_outterTemp) / 100, 4)) / (outterSideTemp - _outterTemp);
        }

        private double CelciasToKelvin(double tempInCelcias) => tempInCelcias + 273.15; 

        private void CalibrateTemps(double avgQl, double ql, int fstTempIndex, int scndTempIndex)
        {
            if (avgQl == ql)
                return;

            var difference = Abs(ql - avgQl);
            var ratio = difference / avgQl / 1000;
            if (ql > avgQl)
            {
                if (fstTempIndex >= 0 && fstTempIndex < _temps.Count)
                    _temps[fstTempIndex] -= _temps[fstTempIndex] * ratio;
                if (scndTempIndex >= 0 && scndTempIndex < _temps.Count)
                    _temps[scndTempIndex] += _temps[scndTempIndex] * ratio;
            }
            else
            {
                if (fstTempIndex >= 0 && fstTempIndex < _temps.Count)
                    _temps[fstTempIndex] += _temps[fstTempIndex] * ratio;
                if (scndTempIndex >= 0 && scndTempIndex < _temps.Count)
                    _temps[scndTempIndex] -= _temps[scndTempIndex] * ratio;
            }
        }
    }
}
