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

namespace ManagedCuda.NvJpeg
{

    /// <summary>
    /// Wrapper class for nvjpegDecodeParams
    /// </summary>
    public class DecodeParams : IDisposable
    {
        private nvjpegDecodeParams _params;
        private NvJpeg _nvJpeg;
        private nvjpegStatus res;
        private bool disposed;

        #region Contructors
        /// <summary>
        /// </summary>
        internal DecodeParams(NvJpeg nvJpeg)
        {
            _nvJpeg = nvJpeg;
            _params = new nvjpegDecodeParams();
            res = NvJpegNativeMethods.nvjpegDecodeParamsCreate(nvJpeg.Handle, ref _params);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsCreate", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }

        /// <summary>
        /// For dispose
        /// </summary>
        ~DecodeParams()
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
                res = NvJpegNativeMethods.nvjpegDecodeParamsDestroy(_params);
                Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsDestroy", res));
                disposed = true;
            }
            if (!fDisposing && !disposed)
                Debug.WriteLine(String.Format("ManagedCUDA not-disposed warning: {0}", this.GetType()));
        }
        #endregion

        /// <summary>
        /// Returns the inner handle.
        /// </summary>
        public nvjpegDecodeParams Params
        {
            get { return _params; }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        // Decode parameters //
        ///////////////////////////////////////////////////////////////////////////////////

        // set output pixel format - same value as in nvjpegDecode()
        public void SetOutputFormat(nvjpegOutputFormat output_format)
        {
            res = NvJpegNativeMethods.nvjpegDecodeParamsSetOutputFormat(_params, output_format);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsSetOutputFormat", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }

        // set to desired ROI. set to (0, 0, -1, -1) to disable ROI decode (decode whole image)
        public void SetROI(int offset_x, int offset_y, int roi_width, int roi_height)
        {
            res = NvJpegNativeMethods.nvjpegDecodeParamsSetROI(_params, offset_x, offset_y, roi_width, roi_height);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsSetROI", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }

        // set to true to allow conversion from CMYK to RGB or YUV that follows simple subtractive scheme
        public void SetAllowCMYK(int allow_cmyk)
        {
            res = NvJpegNativeMethods.nvjpegDecodeParamsSetAllowCMYK(_params, allow_cmyk);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsSetAllowCMYK", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }


        // works only with the hardware decoder backend
        public void SetScaleFactor(nvjpegScaleFactor scale_factor)
        {
            res = NvJpegNativeMethods.nvjpegDecodeParamsSetScaleFactor(_params, scale_factor);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsSetScaleFactor", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }


        /// <summary>
        /// set the orientation flag to the decode parameters
        /// </summary>
        public void SetExifOrientation(nvjpegExifOrientation orientation)
        {
            res = NvJpegNativeMethods.nvjpegDecodeParamsSetExifOrientation(_params, orientation);
            Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "nvjpegDecodeParamsSetExifOrientation", res));
            if (res != nvjpegStatus.Success)
                throw new NvJpegException(res);
        }

    }
}
