// <copyright file="Factory.cs" company="FS Tecnology">
// Copyright (c) FS Tecnology. All rights reserved.
// </copyright>

namespace FManager.Impl
{
    using Core;

    /// <summary>
    /// Base Factory class
    /// </summary>
    public class Factory : IFactory
    {
#pragma warning disable SA1401 // Fields must be private
                              /// <summary>
                              /// Gets or sets UoW dependency
                              /// </summary>
        protected IUnitOfWork unitOfWork;
#pragma warning restore SA1401 // Fields must be private

        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// </summary>
        /// <param name="unitOfWork">The UoW dep</param>
        public Factory(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
