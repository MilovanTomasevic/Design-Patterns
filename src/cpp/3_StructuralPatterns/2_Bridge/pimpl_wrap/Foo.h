#pragma once

#include "pimpl.h"

class Foo
{
  class impl;
  pimpl<impl> impl;
};


