using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Immutable;
using System.Xml.Linq;

namespace tests
{
    [TestClass]
    public class XmlTests
    {
        [TestMethod]
        public void TestXml1()
        {
            var goodEpisodeXml = XElement.Parse(@"
<Episode>
  <id>2673</id>
  <Combined_episodenumber>6</Combined_episodenumber>
  <Combined_season>1</Combined_season>
  <DVD_chapter></DVD_chapter>
  <DVD_discid></DVD_discid>
  <DVD_episodenumber></DVD_episodenumber>
  <DVD_season></DVD_season>
  <Director>|Ellen Brown|</Director>
  <EpImgFlag></EpImgFlag>
  <EpisodeName>930520</EpisodeName>
  <EpisodeNumber>6</EpisodeNumber>
  <FirstAired>1993-05-20</FirstAired>
  <GuestStars></GuestStars>
  <IMDB_ID></IMDB_ID>
  <Language>en</Language>
  <Overview></Overview>
  <ProductionCode></ProductionCode>
  <Rating></Rating>
  <RatingCount>0</RatingCount>
  <SeasonNumber>1</SeasonNumber>
  <Writer></Writer>
  <absolute_number></absolute_number>
  <filename></filename>
  <lastupdated>1163101692</lastupdated>
  <seasonid>71</seasonid>
  <seriesid>70336</seriesid>
  <thumb_added></thumb_added>
  <thumb_height></thumb_height>
  <thumb_width></thumb_width>
</Episode>
            ");

            var modelEpisode = linqtv.Model.Episode.FromXElement(goodEpisodeXml);

            Assert.AreEqual<uint>(modelEpisode.id, 2673);
            Assert.AreEqual(modelEpisode.Combined_episodenumber, 6);
            Assert.AreEqual(modelEpisode.Combined_season, 1);
            Assert.IsNull(modelEpisode.DVD_episodenumber);
            Assert.IsNull(modelEpisode.DVD_season);

            Assert.AreEqual(modelEpisode.Director.Count, 1);
            Assert.AreEqual(modelEpisode.Director[0], "Ellen Brown");

            Assert.IsNull(modelEpisode.EpImgFlag);
            Assert.AreEqual(modelEpisode.EpisodeName, "930520");
            Assert.AreEqual<uint>(modelEpisode.EpisodeNumber, 6);

            Assert.IsNotNull(modelEpisode.FirstAired);
            Assert.AreEqual(modelEpisode.FirstAired.Value.Year, 1993);
            Assert.AreEqual(modelEpisode.FirstAired.Value.Month, 5);
            Assert.AreEqual(modelEpisode.FirstAired.Value.Day, 20);

            Assert.IsNull(modelEpisode.GuestStars);
            Assert.IsNull(modelEpisode.IMDB_ID);
            Assert.AreEqual(modelEpisode.Language, "en");
            Assert.IsNull(modelEpisode.Overview);
            Assert.IsNull(modelEpisode.ProductionCode);
            Assert.IsNull(modelEpisode.Rating);

            Assert.IsTrue(modelEpisode.RatingCount.HasValue);
            Assert.AreEqual<uint>(modelEpisode.RatingCount.Value, 0);

            Assert.IsTrue(modelEpisode.SeasonNumber.HasValue);
            Assert.AreEqual<uint>(modelEpisode.SeasonNumber.Value, 1);

            Assert.IsNull(modelEpisode.Writer);
            Assert.IsNull(modelEpisode.absolute_number);
            Assert.IsNull(modelEpisode.filename);

            Assert.IsNotNull(modelEpisode.lastupdated);
            Assert.AreEqual<long>(modelEpisode.lastupdated.Value.ToUnixTimeSeconds(), 1163101692);

            Assert.IsTrue(modelEpisode.seasonid.HasValue);
            Assert.AreEqual<uint>(modelEpisode.seasonid.Value, 71);

            Assert.AreEqual<uint>(modelEpisode.seriesid, 70336);

            Assert.IsNull(modelEpisode.thumb_added);
            Assert.IsNull(modelEpisode.thumb_height);
            Assert.IsNull(modelEpisode.thumb_width);
        }

        [TestMethod]
        public void TestXml2()
        {
            var goodSeriesXml = XElement.Parse(@"
<Series>
  <id>70336</id>
  <Actors>|Jay Leno|</Actors>
  <Airs_DayOfWeek>Daily</Airs_DayOfWeek>
  <Airs_Time>11:35 PM</Airs_Time>
  <ContentRating></ContentRating>
  <FirstAired>1992-05-25</FirstAired>
  <Genre>|Comedy|Talk Show|</Genre>
  <IMDB_ID>tt0103569</IMDB_ID>
  <Language>en</Language>
  <Network>NBC</Network>
  <NetworkID></NetworkID>
  <Overview>The Tonight Show with Jay Leno is an American late-night talk show currently hosted by Jay Leno, on NBC. It made its debut on May 25, 1992, following Johnny Carson's retirement as host of The Tonight Show. The nightly broadcast at 23:35 (Eastern) originates from NBC's studios, in Burbank, California.</Overview>
  <Rating>7.4</Rating>
  <RatingCount>13</RatingCount>
  <Runtime>60</Runtime>
  <SeriesID>10020</SeriesID>
  <SeriesName>The Tonight Show with Jay Leno</SeriesName>
  <Status>Ended</Status>
  <added></added>
  <addedBy></addedBy>
  <banner>graphical/70336-g.jpg</banner>
  <fanart>fanart/original/70336-1.jpg</fanart>
  <lastupdated>1422299124</lastupdated>
  <poster>posters/70336-2.jpg</poster>
  <tms_wanted_old>1</tms_wanted_old>
  <zap2it_id>SH00004397</zap2it_id>
</Series>

            ");

            var modelSeries = linqtv.Model.Show.FromXElement(goodSeriesXml);
            Assert.AreEqual<uint>(modelSeries.id, 70336);

            Assert.IsNotNull(modelSeries.Actors);
            Assert.AreEqual(modelSeries.Actors[0], "Jay Leno");

            Assert.AreEqual(modelSeries.Airs_DayOfWeek, "Daily");

            Assert.IsTrue(modelSeries.Airs_Time.HasValue);
            Assert.AreEqual(modelSeries.Airs_Time.Value, new TimeSpan(23, 35, 0));

            Assert.IsNull(modelSeries.ContentRating);

            Assert.IsTrue(modelSeries.FirstAired.HasValue);
            Assert.AreEqual(modelSeries.FirstAired.Value, new DateTimeOffset(1992, 5, 25, 0, 0, 0, TimeSpan.Zero));

            Assert.IsTrue(modelSeries.Genre.ToImmutableHashSet().SetEquals(new string[] { "Comedy", "Talk Show" }));

            Assert.AreEqual(modelSeries.IMDB_ID, "tt0103569");
            Assert.AreEqual(modelSeries.Language, "en");
            Assert.AreEqual(modelSeries.Network, "NBC");
            Assert.IsNull(modelSeries.NetworkID);
            Assert.AreEqual(modelSeries.Overview, @"The Tonight Show with Jay Leno is an American late-night talk show currently hosted by Jay Leno, on NBC. It made its debut on May 25, 1992, following Johnny Carson's retirement as host of The Tonight Show. The nightly broadcast at 23:35 (Eastern) originates from NBC's studios, in Burbank, California.");

            Assert.IsTrue(modelSeries.Rating.HasValue);
            Assert.AreEqual(modelSeries.Rating.Value, 7.4f);

            Assert.IsTrue(modelSeries.RatingCount.HasValue);
            Assert.AreEqual<uint>(modelSeries.RatingCount.Value, 13);

            Assert.IsTrue(modelSeries.Runtime.HasValue);
            Assert.AreEqual<uint>(modelSeries.Runtime.Value, 60);

            Assert.AreEqual(modelSeries.SeriesName, @"The Tonight Show with Jay Leno");
            Assert.AreEqual(modelSeries.Status, linqtv.Model.StatusEnum.Ended);
            Assert.IsFalse(modelSeries.added.HasValue);
            Assert.IsFalse(modelSeries.addedBy.HasValue);
            Assert.AreEqual(modelSeries.banner, @"graphical/70336-g.jpg");
            Assert.AreEqual(modelSeries.fanart, @"fanart/original/70336-1.jpg");
            Assert.AreEqual(modelSeries.lastupdated, DateTimeOffset.FromUnixTimeSeconds(1422299124));
            Assert.AreEqual(modelSeries.poster, @"posters/70336-2.jpg");
            Assert.AreEqual(modelSeries.zap2it_id, @"SH00004397");
        }
    }
}