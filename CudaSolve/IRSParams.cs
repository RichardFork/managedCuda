﻿// Copyright (c) 2023, Michael Kunz and Artic Imaging SARL. All rights reserved.
// http://kunzmi.github.io/managedCuda
//
// This file is part of ManagedCuda.
//
// Commercial License Usage
//  Licensees holding valid commercial ManagedCuda licenses may use this
//  file in accordance with the commercial license agreement provided with
//  the Software or, alternatively, in accordance with the terms contained
//  in a written agreement between you and Artic Imaging SARL. For further
//  information contact us at managedcuda@articimaging.eu.
//  
// GNU General Public License Usage
//  Alternatively, this file may be used under the terms of the GNU General
//  Public License as published by the Free Software Foundation, either 
//  version 3 of the License, or (at your option) any later version.
//  
//  ManagedCuda is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Diagnostics;

namespace ManagedCuda.CudaSolve
{
    /// <summary>
    /// 
    /// </summary>
    public class IRSParams : IDisposable
    {
        private cusolverDnIRSParams _params;
        private cusolverStatus res;
        private bool disposed;

        #region Contructors
        /// <summary>
        /// </summary>
        public IRSParams()
        {
            _params = new cusolverDnIRSParams();
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsCreate(ref _params);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsCreate", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// For dispose
        /// </summary>
        ~IRSParams()
        {
            Dispose(false);
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// For IDisposable
        /// </summary>
        /// <param name="fDisposing"></param>
        protected virtual void Dispose(bool fDisposing)
        {
            if (fDisposing && !disposed)
            {
                //Ignore if failing
                res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsDestroy(_params);
                Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsDestroy", res));
                disposed = true;
            }
            if (!fDisposing && !disposed)
                Debug.WriteLine(String.Format("ManagedCUDA not-disposed warning: {0}", this.GetType()));
        }
        #endregion

        /// <summary>
        /// Returns the inner handle.
        /// </summary>
        public cusolverDnIRSParams Params
        {
            get { return _params; }
        }


        /// <summary>
        /// </summary>
        public void SetTol(double val)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetTol(_params, val);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetTol", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetTolInner(double val)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetTolInner(_params, val);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetTolInner", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetSolverPrecisions(cusolverPrecType solver_main_precision, cusolverPrecType solver_lowest_precision)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetSolverPrecisions(_params, solver_main_precision, solver_lowest_precision);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetSolverPrecisions", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetRefinementSolver(cusolverIRSRefinement refinement_solver)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetRefinementSolver(_params, refinement_solver);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetRefinementSolver", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetMaxIters(int maxiters)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetMaxIters(_params, maxiters);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetMaxIters", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetMaxItersInner(int maxiters)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetMaxItersInner(_params, maxiters);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetMaxItersInner", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public int GetMaxIters()
        {
            int val = 0;
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsGetMaxIters(_params, ref val);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsGetMaxIters", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
            return val;
        }

        /// <summary>
        /// </summary>
        public void SetSolverMainPrecision(cusolverPrecType solver_main_precision)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetSolverMainPrecision(_params, solver_main_precision);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetSolverMainPrecision", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void SetSolverLowestPrecision(cusolverPrecType solver_main_precision)
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsSetSolverLowestPrecision(_params, solver_main_precision);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsSetSolverLowestPrecision", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void EnableFallback()
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsEnableFallback(_params);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsEnableFallback", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }

        /// <summary>
        /// </summary>
        public void DisableFallback()
        {
            res = CudaSolveNativeMethods.Dense.cusolverDnIRSParamsDisableFallback(_params);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnIRSParamsDisableFallback", res));
            if (res != cusolverStatus.Success)
                throw new CudaSolveException(res);
        }
    }
}
