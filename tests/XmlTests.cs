using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Immutable;

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
    }
}
