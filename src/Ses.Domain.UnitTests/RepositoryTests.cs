﻿using System;
using System.Collections.Generic;
using System.Threading;
using Ses.Domain.UnitTests.Fakes;
using FakeItEasy;
using Ses.Abstracts;
using Xunit;

namespace Ses.Domain.UnitTests
{
    public class RepositoryTests
    {
        [Fact]
        public async void Loaded_aggregate_from_eventstore_when_stream_not_exists_throws()
        {
            var streamId = Guid.Empty;

            var store = A.Fake<IEventStore>();
            A.CallTo(() => store.Load(streamId, false, CancellationToken.None)).Returns((IReadOnlyEventStream)null);

            var repo = new Repository<FakeAggregate>(store);

            await Assert.ThrowsAsync<AggregateNotFoundException>(async () =>
            {
                await repo.Load(streamId);
            });
        }

        [Fact]
        public async void Loaded_aggregate_from_eventstore_with_one_event_return_committed_version_equals_1()
        {
            var streamId = Guid.Empty;
            var events = new List<FakeEvent> { new FakeEvent() };
            var committedVersion = events.Count;

            var store = A.Fake<IEventStore>();
            A.CallTo(() => store.Load(streamId, false, CancellationToken.None))
                .Returns(new ReadOnlyEventStream(events, committedVersion));

            var repo = new Repository<FakeAggregate>(store);
            var aggregate = await repo.Load(streamId);

            Assert.Equal(1, aggregate.CommittedVersion);
        }
    }
}