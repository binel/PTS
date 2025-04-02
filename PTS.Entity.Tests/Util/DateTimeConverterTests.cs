namespace PTS.Entity.Tests.Util;

using PTS.Entity.Util;

class DateTimeConverterTests {

    [Test]
    public void FromUnix() {
        DateTime date = DateTimeConverter.FromUnix(1743564344);

        Assert.That(date.Month, Is.EqualTo(4));
    }
}