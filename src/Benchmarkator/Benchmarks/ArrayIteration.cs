﻿using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Benchmarks.Array
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class ArrayIteration
    {
        [Params(4096)]
        public int Length;

        private int[] _data;

        [GlobalSetup]
        public void Setup()
        {
            _data = Enumerable.Range(0, Length).ToArray();
        }

        [Benchmark]
        public int ForLoopAccessIndex()
        {
            var item = 0;
            for (var i = 0; i < _data.Length; i++)
            {
                item = _data[i];
            }

            return item;
        }

        [Benchmark]
        public int ForLoopAccessIndexLocal()
        {
            var arr = _data;
            var item = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                item = arr[i];
            }

            return item;
        }

        [Benchmark]
        public unsafe int ForLoopAccessPtr()
        {
            var item = 0;

            // to get value on given index, `i` is added to address of first item
            // (`i` offsets pointer by size of `int*`)
            fixed (int* a = &_data[0])
            {
                for (var i = 0; i < _data.Length; i++)
                {
                    item = *(a + i);
                }
            }

            return item;
        }

        [Benchmark]
        public int ForLoopAccessRef()
        {
            var item = 0;

            // to get value on given index, `i` is added to reference to first item
            // (`i` offsets pointer by size of `ref int`)
            ref var first = ref _data[0];
            for (var i = 0; i < _data.Length; i++)
            {
                item = Unsafe.Add(ref first, i);
            }

            return item;
        }

        [Benchmark]
        public unsafe int WhileLoopAccessPtr()
        {
            var item = 0;

            // move `tmp` in array, from first to last address
            // (pointer to `tmp` is moved to next field each iteration)
            fixed (int* a = &_data[0])
            {
                var last = a + _data.Length;
                var tmp = a;
                while (tmp < last)
                {
                    item = *tmp;
                    tmp += 1;
                }
            }

            return item;
        }

        [Benchmark]
        public int WhileLoopAccessRef()
        {
            var item = 0;

            // move `temp` in array, from first to last reference
            // (reference to `tmp` is moved to next field each iteration)
            ref var temp = ref _data[0];
            ref var last = ref _data[_data.Length - 1];
            while (Unsafe.IsAddressLessThan(ref temp, ref last) || Unsafe.AreSame(ref temp, ref last))
            {
                item = temp;
                temp = ref Unsafe.Add(ref temp, 1);
            }

            return item;
        }
    }
}
