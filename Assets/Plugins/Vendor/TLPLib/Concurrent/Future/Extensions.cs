﻿using System;
using com.tinylabproductions.TLPLib.Functional;
using com.tinylabproductions.TLPLib.Reactive;

namespace com.tinylabproductions.TLPLib.Concurrent {
  public static class FutureExts {
    public static Future<Either<Err, To>> mapE<From, To, Err>(
      this Future<Either<Err, From>> future, Fn<From, To> mapper
    ) { return future.map(e => e.mapRight(mapper)); }

    public static Future<Either<Err, To>> mapE<From, To, Err>(
      this Future<Either<Err, From>> future, Fn<From, Either<Err, To>> mapper
    ) { return future.map(e => e.flatMapRight(mapper)); }

    public static Future<Option<To>> mapO<From, To>(
      this Future<Option<From>> future, Fn<From, To> mapper
    ) { return future.map(opt => opt.map(mapper)); }

    public static Future<Option<B>> flatMapO<A, B>(
      this Future<Option<A>> future, Fn<A, Future<Option<B>>> mapper
    ) {
      return future.flatMap(opt => opt.fold(
        () => Future.successful(F.none<B>()),
        mapper
      ));
    }

    public static Future<Either<Err, To>> flatMapE<From, To, Err>(
      this Future<Either<Err, From>> future, Fn<From, Future<To>> mapper
    ) {
      return future.flatMap(e => e.fold(
        err => Future.successful(Either<Err, To>.Left(err)),
        from => mapper(from).map(Either<Err, To>.Right)
      ));
    }

    public static Future<Either<Err, To>> flatMapE<From, To, Err>(
      this Future<Either<Err, From>> future, Fn<From, Future<Either<Err, To>>> mapper
    ) {
      return future.flatMap(e => e.fold(
        err => Future.successful(Either<Err, To>.Left(err)),
        mapper
      ));
    }

    public static Future<Try<To>> flatMapT<From, To>(
      this Future<Try<From>> future, Fn<From, Future<To>> mapper
    ) {
      return future.flatMap(t => t.fold(
        from => mapper(from).map(F.scs),
        err => Future.successful(F.err<To>(err))
      ));
    }

    public static Future<A> flatten<A>(this Future<Future<A>> future) =>
      future.flatMap(_ => _);

    /** Complete the future with the right side, never complete if left side occurs. **/
    public static Future<B> dropError<A, B>(this Future<Either<A, B>> future) {
      return Future.a<B>(p => future.onSuccess(p.complete));
    }

    public static IRxVal<Option<A>> toRxVal<A>(this Future<A> future) {
      var rx = RxRef.a(F.none<A>());
      future.onComplete(a => rx.value = F.some(a));
      return rx;
    }

    public static IRxVal<A> toRxVal<A>(
      this Future<IRxVal<A>> future, A whileNotCompleted
    ) {
      var rx = RxRef.a(whileNotCompleted);
      future.onComplete(rx2 => rx2.subscribe(v => rx.value = v));
      return rx;
    }

    public static Future<A> extract<A>(this Option<Future<A>> futureOpt) =>
      futureOpt.fold(Future<A>.unfulfilled, f => f);

    public static Future<A> extract<A>(this Future<Option<A>> optFuture) =>
      optFuture.flatMap(opt => opt.fold(Future<A>.unfulfilled, Future.successful));

    public static Future<Option<A>> extractOpt<A>(this Option<Future<A>> futureOpt) =>
      futureOpt.fold(() => Future.successful(F.none<A>()), f => f.map(F.some));

    public static void onSuccess<A, B>(this Future<Either<A, B>> future, Act<B> action) => 
      future.onComplete(e => {
        foreach (var b in e.rightValue) action(b);
      });

    public static void onSuccess<A>(this Future<Try<A>> future, Act<A> action) => 
      future.onComplete(e => {
        foreach (var a in e.value) action(a);
      });

    public static Future<Option<B>> ofSuccess<A, B>(this Future<Either<A, B>> future) => 
      future.map(e => e.rightValue);

    public static Future<Option<A>> ofSuccess<A>(this Future<Try<A>> future) => 
      future.map(e => e.value);

    public static void onFailure<A, B>(this Future<Either<A, B>> future, Act<A> action) => 
      future.onComplete(e => {
        foreach (var a in e.leftValue) action(a);
      });

    public static void onFailure<A>(this Future<Try<A>> future, Act<Exception> action) => 
      future.onComplete(e => {
        foreach (var ex in e.exception) action(ex);
      });

    public static Future<Option<A>> ofFailure<A, B>(this Future<Either<A, B>> future) => 
      future.map(e => e.leftValue);

    public static Future<Option<Exception>> ofFailure<A>(this Future<Try<A>> future) => 
      future.map(e => e.exception);

    /**
     * Delays completing of given future until the returned action is called.
     **/
    public static Tpl<Future<A>, Action> delayUntilSignal<A>(this Future<A> future) {
      Promise<Unit> signalP;
      var f = future.zip(Future<Unit>.async(out signalP), (a, _) => a);
      Action act = () => signalP.tryComplete(F.unit);
      return F.t(f, act);
    }

    /** Converts option into successful/unfulfilled future. */
    public static Future<A> toFuture<A>(this Option<A> opt) {
      foreach (var a in opt) return Future.successful(a);
      return Future<A>.unfulfilled;
    }

    public static Functional.Lazy<Option<A>> toLazy<A>(this Future<A> f) =>
      F.lazy(() => f.value);
  }
}
