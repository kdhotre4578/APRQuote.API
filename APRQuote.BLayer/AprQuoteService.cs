using System.Collections.Generic;
using APRQuote.Core.Contracts;
using APRQuote.Core.Models;
using System.Linq;
using System;

namespace APRQuote.BLayer
{
    public class AprQuoteService : IAprQuote
    {
        private readonly IRepository<Vehicle> _vehicleRepos;
        private readonly IRepository<QuoteType> _quoteTypeRepos;
        private readonly IRepository<APRPercentRange> _aprPercentRangeRepos;
        private readonly IRepository<Quote> _quoteRepos;
        private readonly IAprUoW _aprUoW;

        public AprQuoteService(IAprUoW aprUow)
        {
            _vehicleRepos = aprUow.VehicleRepository;
            _quoteTypeRepos = aprUow.QuoteTypeRepository;
            _aprPercentRangeRepos = aprUow.APRPercentRangeRepository;
            _quoteRepos = aprUow.QuoteRepository;

            this._aprUoW = aprUow;
        }

        /// <summary>
        /// Gets all APR Percentage range quotes with vehicle and quote type details
        /// </summary>
        /// <returns>quotes</returns>
        public IEnumerable<AprQuote> GetAllQuotes()
        {
            IEnumerable<AprQuote> aprQuotes = (from q in _quoteRepos.Get()
                          join v in _vehicleRepos.Get() on q.VehicleId equals v.Id 
                          join qt in _quoteTypeRepos.Get() on q.QuoteTypeId equals qt.Id 
                          join apr in _aprPercentRangeRepos.Get() on q.APRPercentRangeId equals apr.Id
                          select new AprQuote
                          {
                              Id = q.Id,
                              Make = v.Make,
                              VehicleType = v.VehicleType,
                              QuoteType = qt.Type,
                              ZeroThreeMonths = apr.ZeroThreeMonths,
                              ThreeSixMonths = apr.ThreeSixMonths,
                              SixTwelveMonths = apr.SixTwelveMonths,
                              TwelvePlusMonths = apr.TwelvePlusMonths
                          }).ToList();

            return aprQuotes;
        }

        /// <summary>
        /// Gets APR Percentage range quote with vehicle and quote type details as per given Id
        /// </summary>
        /// <returns>quote</returns>
        public AprQuote GetQuote(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            AprQuote quote = (from q in _quoteRepos.Get()
                    join v in _vehicleRepos.Get() on q.VehicleId equals v.Id
                    join qt in _quoteTypeRepos.Get() on q.QuoteTypeId equals qt.Id
                    join apr in _aprPercentRangeRepos.Get() on q.APRPercentRangeId equals apr.Id
                    where q.Id == id
                    select new AprQuote
                    {
                        Id = q.Id,
                        Make = v.Make,
                        VehicleType = v.VehicleType,
                        QuoteType = qt.Type,
                        ZeroThreeMonths = apr.ZeroThreeMonths,
                        ThreeSixMonths = apr.ThreeSixMonths,
                        SixTwelveMonths = apr.SixTwelveMonths,
                        TwelvePlusMonths = apr.TwelvePlusMonths
                    }).FirstOrDefault();
            
            return quote;
        }

        /// <summary>
        /// Verifies and adds quote to the database
        /// </summary>
        /// <param name="aprQuote">quote</param>
        /// <returns>successfully added</returns>
        public bool AddQuote(AprQuote aprQuote)
        {
            bool saveChanges = false;

            try
            {
                if (aprQuote == null)
                {
                    return saveChanges;
                }

                // Vehicle

                Vehicle vehicle = _vehicleRepos.Get(v => v.Make == aprQuote.Make && v.VehicleType == aprQuote.VehicleType);

                if (vehicle == null)
                {
                    vehicle = new Vehicle() { Make = aprQuote.Make, VehicleType = aprQuote.VehicleType };
                    _vehicleRepos.Add(vehicle);
                    saveChanges = true;
                }

                // QuoteType

                QuoteType quoteType = _quoteTypeRepos.Get(q => q.Type == aprQuote.QuoteType);

                if (quoteType == null)
                {
                    quoteType = new QuoteType() { Type = aprQuote.QuoteType };
                    _quoteTypeRepos.Add(quoteType);
                    saveChanges = true;
                }

                // APR Percent Ranges

                APRPercentRange aprPercentRange = _aprPercentRangeRepos.Get(a => a.ZeroThreeMonths == aprQuote.ZeroThreeMonths
                                        && a.ThreeSixMonths == aprQuote.ThreeSixMonths
                                        && a.SixTwelveMonths == aprQuote.SixTwelveMonths
                                        && a.TwelvePlusMonths == aprQuote.TwelvePlusMonths);

                if (aprPercentRange == null)
                {
                    aprPercentRange = new APRPercentRange()
                    {
                        ZeroThreeMonths = aprQuote.ZeroThreeMonths,
                        ThreeSixMonths = aprQuote.ThreeSixMonths,
                        SixTwelveMonths = aprQuote.SixTwelveMonths,
                        TwelvePlusMonths = aprQuote.TwelvePlusMonths
                    };

                    _aprPercentRangeRepos.Add(aprPercentRange);

                    saveChanges = true;
                }

                if (saveChanges)
                {
                    _aprUoW.Commit();
                }

                // Quote

                Quote quote = _quoteRepos.Get(q => q.VehicleId == vehicle.Id
                                && q.QuoteTypeId == quoteType.Id
                                && q.APRPercentRangeId == aprPercentRange.Id);

                if (quote == null)
                {
                    quote = new Quote()
                    {
                        VehicleId = vehicle.Id,
                        QuoteTypeId = quoteType.Id,
                        APRPercentRangeId = aprPercentRange.Id
                    };

                    _quoteRepos.Add(quote);
                    saveChanges = true;

                    _aprUoW.Commit();
                }
            }
            catch(Exception ex) 
            {
                _aprUoW.Dispose();
                saveChanges = false;
            }

            return saveChanges;
        }
    }
}
