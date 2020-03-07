using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AutomaticTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AutomaticModeHappyPath()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Fail("failed");
            validator.onError += err => Assert.Fail(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Pass("passed");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.PiecePlaced, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            validator.Validate(TaskType.ResetPressed, true);
            validator.Validate(TaskType.CycleStartPressed, true);
            validator.Validate(TaskType.PieceInBox, true);
            Assert.Fail("no event triggered");
        }

        [Test]
        public void ShouldCauseErrorCycleStartPressedNoReset()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Fail();
            validator.onError += err => Assert.Pass(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("Simulation Succeeded");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.PiecePlaced, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            validator.Validate(TaskType.CycleStartPressed, true);

            Assert.Fail("no event triggered");
        }


        [Test]
        public void ShouldFailResetPressedWithNoPiece()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Pass();
            validator.onError += err => Debug.Log(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("Simulation Succeeded");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            validator.Validate(TaskType.ResetPressed, true);
            Assert.Fail("no event triggered");
        }


        [Test]
        public void ShouldFailPassingLightRays()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Pass();
            validator.onError += err => Debug.Log(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("Simulation Succeeded");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, false);
            Assert.Fail("no event triggered");
        }

        [Test]
        public void ShouldFailInterruptorManual()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Pass();
            validator.onError += err => Debug.Log(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("Simulation Succeeded");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.PiecePlaced, true);
            validator.Validate(TaskType.InterruptorManual, true);
            Assert.Fail("no event triggered");
        }

        [Test]
        public void ShouldFailResetPressedDoorOpen()
        {
            AutomaticChecklist checklist = new AutomaticChecklist();
            AutomaticChecklistValidator validator = new AutomaticChecklistValidator(checklist);

            validator.onFail += () => Assert.Pass();
            validator.onError += err => Debug.Log(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("Simulation Succeeded");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.PiecePlaced, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            validator.Validate(TaskType.ResetPressed, true);
            Assert.Fail("no event triggered");
        }
    }
}
