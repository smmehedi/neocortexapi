﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using NeoCortexApi.Encoders;
using NeoCortexApi.Network;
using NeoCortexApi;
using NeoCortexApi.Entities;
using System.Diagnostics;

namespace UnitTestsProject
{   
    [TestClass]
    public class NetworkTests
    {

        /// <summary>
        /// Initializes encoder and invokes Encode method.
        /// </summary>
        [TestMethod]
        [TestCategory("NetworkTests")]
        public void InitTests()
        {
            bool learn = true;
            Parameters p = Parameters.getAllDefaultParameters();
            string[] categories = new string[] {"A", "B", "C", "D","E","F","G","H","J","K","L","M","N","O","P","Q","R","S","T","U"};
            CortexNetwork net = new CortexNetwork("my cortex");
            List<CortexRegion> regions = new List<CortexRegion>();
            CortexRegion region0 = new CortexRegion("1st Region");
            regions.Add(region0);
            
            SpatialPooler sp1 = new SpatialPooler();
            TemporalMemory tm1 = new TemporalMemory();
            var mem = new Connections();
            p.apply(mem);
            sp1.init(mem);
            tm1.init(mem);
            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("W", 5);
            //settings.Add("N", 100);
            settings.Add("Radius", 1);
            
            EncoderBase encoder = new CategoryEncoder(categories, settings);
            //encoder.Encode()
            CortexLayer<object, object> layer1 = new CortexLayer<object,object>("L1");
            region0.AddLayer(layer1);
            layer1.HtmModules.Add(encoder);
            layer1.HtmModules.Add(sp1);
            layer1.HtmModules.Add(tm1);
            //layer1.Compute();
            
            string[] inputs = new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O"};
            for (int i = 0; i < 1; i++)
            {
                foreach (var input in inputs)
                {
                    layer1.Compute((object)input, learn);
                    Debug.WriteLine("-----------------------------------------------------------\n----------------------------------------------------------");
                }
            }
            //Debug.WriteLine("------------------------------------------------------------------------\n----------------------------------------------------------------------------");
            /*
            learn = false;
            for (int i = 0; i < 19; i++)
            {
                foreach (var input in inputs)
                {
                    layer1.Compute((object)input, learn);
                }
            }
            */
            
        }

    }
}